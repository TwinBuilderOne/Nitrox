﻿using System;
using System.Collections.Generic;
using System.Linq;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.GameLogic.Entities;
using NitroxModel.DataStructures.Util;
using NitroxModel.Logger;
using NitroxServer.Serialization;
using NitroxServer.Serialization.Resources.Datastructures;

namespace NitroxServer.GameLogic.Entities.Spawning
{
    public class BatchEntitySpawner : IEntitySpawner
    {
        private readonly BatchCellsParser batchCellsParser;

        private readonly Dictionary<NitroxTechType, IEntityBootstrapper> customBootstrappersByTechType;
        private readonly HashSet<NitroxInt3> emptyBatches = new HashSet<NitroxInt3>();
        private readonly Dictionary<string, PrefabPlaceholdersGroupAsset> prefabPlaceholderGroupsbyClassId;
        private readonly UwePrefabFactory prefabFactory;

        private readonly string seed;

        private readonly UweWorldEntityFactory worldEntityFactory;

        private readonly object parsedBatchesLock = new object();
        private readonly object emptyBatchesLock = new object();
        private HashSet<NitroxInt3> parsedBatches;

        public List<NitroxInt3> SerializableParsedBatches
        {
            get
            {
                List<NitroxInt3> parsed;
                List<NitroxInt3> empty;

                lock (parsedBatchesLock)
                {
                    parsed = new List<NitroxInt3>(parsedBatches);
                }

                lock (emptyBatchesLock)
                {
                    empty = new List<NitroxInt3>(emptyBatches);
                }

                return parsed.Except(empty).ToList();
            }
            set
            {
                lock (parsedBatchesLock)
                {
                    parsedBatches = new HashSet<NitroxInt3>(value);
                }
            }
        }

        public BatchEntitySpawner(EntitySpawnPointFactory entitySpawnPointFactory, UweWorldEntityFactory worldEntityFactory, UwePrefabFactory prefabFactory, List<NitroxInt3> loadedPreviousParsed, ServerProtoBufSerializer serializer,
                                  Dictionary<NitroxTechType, IEntityBootstrapper> customBootstrappersByTechType, Dictionary<string, PrefabPlaceholdersGroupAsset> prefabPlaceholderGroupsbyClassId, string seed)
        {
            parsedBatches = new HashSet<NitroxInt3>(loadedPreviousParsed);
            this.worldEntityFactory = worldEntityFactory;
            this.prefabFactory = prefabFactory;
            this.customBootstrappersByTechType = customBootstrappersByTechType;
            this.prefabPlaceholderGroupsbyClassId = prefabPlaceholderGroupsbyClassId;
            this.seed = seed;
            batchCellsParser = new BatchCellsParser(entitySpawnPointFactory, serializer);
        }

        public List<Entity> LoadUnspawnedEntities(NitroxInt3 batchId)
        {
            lock (parsedBatches)
            {
                if (parsedBatches.Contains(batchId))
                {
                    return new List<Entity>();
                }

                parsedBatches.Add(batchId);
            }

            DeterministicBatchGenerator deterministicBatchGenerator = new DeterministicBatchGenerator(seed, batchId);
            List<EntitySpawnPoint> spawnPoints = batchCellsParser.ParseBatchData(batchId);
            List<Entity> entities = SpawnEntities(spawnPoints, deterministicBatchGenerator);

            if (entities.Count == 0)
            {
                lock (emptyBatchesLock)
                {
                    emptyBatches.Add(batchId);
                }
            }
            else
            {
                Log.Info("Spawning " + entities.Count + " entities from " + spawnPoints.Count + " spawn points in batch " + batchId);
            }

            for (int x = 0; x < entities.Count; x++) // Throws on duplicate Entities already but nice to know which ones
            {
                for (int y = 0; y < entities.Count; y++)
                {
                    if (entities[x] == entities[y] && x != y)
                    {
                        Log.Error("Duplicate Entity detected! " + entities[x]);
                    }
                }
            }

            return entities;
        }

        private IEnumerable<Entity> SpawnEntitiesUsingRandomDistribution(EntitySpawnPoint entitySpawnPoint, List<UwePrefab> prefabs, DeterministicBatchGenerator deterministicBatchGenerator, WorldEntity parentEntity = null)
        {
            List<UwePrefab> allowedPrefabs = FilterAllowedPrefabs(prefabs, entitySpawnPoint);

            float rollingProbabilityDensity = allowedPrefabs.Sum(prefab => prefab.Probability / entitySpawnPoint.Density);

            if (rollingProbabilityDensity <= 0)
            {
                yield break;
            }

            double randomNumber = deterministicBatchGenerator.NextDouble();
            if (rollingProbabilityDensity > 1f)
            {
                randomNumber *= rollingProbabilityDensity;
            }

            double rollingProbability = 0;

            UwePrefab selectedPrefab = allowedPrefabs.FirstOrDefault(prefab =>
            {
                if (Math.Abs(prefab.Probability) < 0.0001)
                {
                    return false;
                }

                float probabilityDensity = prefab.Probability / entitySpawnPoint.Density;

                rollingProbability += probabilityDensity;

                return rollingProbability >= randomNumber;
            });

            if (selectedPrefab == null)
            {
                yield break;
            }

            Optional<UweWorldEntity> opWorldEntity = worldEntityFactory.From(selectedPrefab.ClassId);

            if (opWorldEntity.HasValue)
            {
                UweWorldEntity uweWorldEntity = opWorldEntity.Value;

                for (int i = 0; i < selectedPrefab.Count; i++)
                {
                    IEnumerable<Entity> entities = CreateEntityWithChildren(entitySpawnPoint,
                                                                            uweWorldEntity.Scale,
                                                                            uweWorldEntity.TechType,
                                                                            uweWorldEntity.CellLevel,
                                                                            selectedPrefab.ClassId,
                                                                            deterministicBatchGenerator,
                                                                            parentEntity);
                    foreach (Entity entity in entities)
                    {
                        yield return entity;
                    }
                }
            }
        }

        private List<UwePrefab> FilterAllowedPrefabs(List<UwePrefab> prefabs, EntitySpawnPoint entitySpawnPoint)
        {
            List<UwePrefab> allowedPrefabs = new List<UwePrefab>();

            foreach (UwePrefab prefab in prefabs)
            {
                if (prefab.ClassId != "None")
                {
                    Optional<UweWorldEntity> uweWorldEntity = worldEntityFactory.From(prefab.ClassId);

                    if (uweWorldEntity.HasValue && entitySpawnPoint.AllowedTypes.Contains(uweWorldEntity.Value.SlotType))
                    {
                        allowedPrefabs.Add(prefab);
                    }
                }
            }

            return allowedPrefabs;
        }

        private IEnumerable<Entity> SpawnEntitiesStaticly(EntitySpawnPoint entitySpawnPoint, DeterministicBatchGenerator deterministicBatchGenerator, WorldEntity parentEntity = null)
        {
            Optional<UweWorldEntity> uweWorldEntity = worldEntityFactory.From(entitySpawnPoint.ClassId);

            if (uweWorldEntity.HasValue)
            {
                IEnumerable<Entity> entities = CreateEntityWithChildren(entitySpawnPoint,
                                                                        entitySpawnPoint.Scale,
                                                                        uweWorldEntity.Value.TechType,
                                                                        uweWorldEntity.Value.CellLevel,
                                                                        entitySpawnPoint.ClassId,
                                                                        deterministicBatchGenerator,
                                                                        parentEntity);
                foreach (Entity entity in entities)
                {
                    yield return entity;
                }
            }
        }

        private IEnumerable<Entity> CreateEntityWithChildren(EntitySpawnPoint entitySpawnPoint, NitroxVector3 scale, NitroxTechType techType, int cellLevel, string classId, DeterministicBatchGenerator deterministicBatchGenerator, WorldEntity parentEntity = null)
        {
            WorldEntity spawnedEntity = new WorldEntity(entitySpawnPoint.LocalPosition,
                                                        entitySpawnPoint.LocalRotation,
                                                        scale,
                                                        techType,
                                                        cellLevel,
                                                        classId,
                                                        true,
                                                        deterministicBatchGenerator.NextId(),
                                                        parentEntity,
                                                        false,
                                                        null);

            spawnedEntity.Children = SpawnEntities(entitySpawnPoint.Children, deterministicBatchGenerator, spawnedEntity);

            CreatePrefabPlaceholdersWithChildren(spawnedEntity, classId, deterministicBatchGenerator);

            IEntityBootstrapper bootstrapper;

            if (customBootstrappersByTechType.TryGetValue(techType, out bootstrapper))
            {
                bootstrapper.Prepare(spawnedEntity, parentEntity, deterministicBatchGenerator);
            }

            yield return spawnedEntity;

            if (parentEntity == null) // Ensures children are only returned at the top level
            {
                // Children are yielded as well so they can be indexed at the top level (for use by simulation 
                // ownership and various other consumers).  The parent should always be yielded before the children
                foreach (Entity childEntity in AllChildren(spawnedEntity))
                {
                    yield return childEntity;
                }
            }
        }

        private IEnumerable<Entity> AllChildren(Entity entity)
        {
            foreach (Entity child in entity.Children)
            {
                yield return child;

                if (child.Children.Count > 0)
                {
                    foreach (Entity childOfChild in AllChildren(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private List<Entity> SpawnEntities(List<EntitySpawnPoint> entitySpawnPoints, DeterministicBatchGenerator deterministicBatchGenerator, WorldEntity parentEntity = null)
        {
            List<Entity> entities = new List<Entity>();
            foreach (EntitySpawnPoint esp in entitySpawnPoints)
            {
                if (esp.Density > 0)
                {
                    List<UwePrefab> prefabs = prefabFactory.GetPossiblePrefabs(esp.BiomeType);

                    if (prefabs.Count > 0)
                    {
                        entities.AddRange(SpawnEntitiesUsingRandomDistribution(esp, prefabs, deterministicBatchGenerator, parentEntity));
                    }
                    else if (esp.ClassId != null)
                    {
                        entities.AddRange(SpawnEntitiesStaticly(esp, deterministicBatchGenerator, parentEntity));
                    }
                }
            }
            return entities;
        }

        private void CreatePrefabPlaceholdersWithChildren(WorldEntity entity, string classId, DeterministicBatchGenerator deterministicBatchGenerator)
        {
            PrefabPlaceholdersGroupAsset group;

            // Check to see if this entity is a PrefabPlaceholderGroup.  If it is, 
            // we want to add the children that would be spawned here.  This is 
            // surpressed on the client so we don't get virtual entities that the
            // server doesn't know about.
            if (prefabPlaceholderGroupsbyClassId.TryGetValue(classId, out group))
            {
                foreach (PrefabAsset prefab in group.SpawnablePrefabs)
                {
                    TransformAsset transform = prefab.TransformAsset;

                    Optional<UweWorldEntity> opWorldEntity = worldEntityFactory.From(prefab.ClassId);

                    if (!opWorldEntity.HasValue)
                    {
                        Log.Debug("Unexpected Empty WorldEntity! " + prefab.ClassId);
                        continue;
                    }

                    UweWorldEntity worldEntity = opWorldEntity.Value;
                    WorldEntity prefabEntity = new WorldEntity(transform.LocalPosition,
                                                               transform.LocalRotation,
                                                               transform.LocalScale,
                                                               worldEntity.TechType,
                                                               worldEntity.CellLevel,
                                                               prefab.ClassId,
                                                               true,
                                                               deterministicBatchGenerator.NextId(),
                                                               entity,
                                                               false,
                                                               null);

                    if (prefab.EntitySlot.HasValue)
                    {
                        Entity possibleEntity = SpawnEntitySlotEntities(prefab.EntitySlot.Value, transform, deterministicBatchGenerator, entity);
                        if (possibleEntity != null)
                        {
                            entity.Children.Add(possibleEntity);
                        }
                    }

                    CreatePrefabPlaceholdersWithChildren(prefabEntity, prefabEntity.ClassId, deterministicBatchGenerator);
                    entity.Children.Add(prefabEntity);
                }

                entity.Children.AddRange(ConvertComponentPrefabsToEntities(group.ExistingPrefabs, entity, deterministicBatchGenerator));
            }
        }

        // Entities that have been spawned by a parent prefab (child game objects baked into the prefab).
        // created separately as we don't actually want to spawn these but instead just update the id.
        // will refactor this piece a bit later to split these into a new data structure.
        private List<Entity> ConvertComponentPrefabsToEntities(List<PrefabAsset> prefabs, Entity parent, DeterministicBatchGenerator deterministicBatchGenerator)
        {
            List<Entity> entities = new List<Entity>();

            int counter = 0;

            foreach (PrefabAsset prefab in prefabs)
            {
                TransformAsset transform = prefab.TransformAsset;

                PrefabChildEntity prefabEntity = new PrefabChildEntity(deterministicBatchGenerator.NextId(), new NitroxTechType("None"), counter++, parent);
                                       
                prefabEntity.Children = ConvertComponentPrefabsToEntities(prefab.Children, prefabEntity, deterministicBatchGenerator);
                
                entities.Add(prefabEntity);
            }

            return entities;
        }

        private Entity SpawnEntitySlotEntities(NitroxEntitySlot entitySlot, TransformAsset transform, DeterministicBatchGenerator deterministicBatchGenerator, WorldEntity parentEntity)
        {
            List<UwePrefab> prefabs = prefabFactory.GetPossiblePrefabs(entitySlot.BiomeType);
            List<Entity> entities = new List<Entity>();

            if (prefabs.Count > 0)
            {
                EntitySpawnPoint entitySpawnPoint = new EntitySpawnPoint(parentEntity.AbsoluteEntityCell, transform.LocalPosition, transform.LocalRotation, entitySlot.AllowedTypes.ToList(), 1f, entitySlot.BiomeType);
                entities.AddRange(SpawnEntitiesUsingRandomDistribution(entitySpawnPoint, prefabs, deterministicBatchGenerator, parentEntity));
            }

            return entities.FirstOrDefault();
        }
    }
}
