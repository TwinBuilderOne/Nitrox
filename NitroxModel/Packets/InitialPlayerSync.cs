using System;
using System.Collections.Generic;
using System.Linq;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Unity;
using NitroxModel.DataStructures.Util;
using NitroxModel.Server;

namespace NitroxModel.Packets
{
    [Serializable]
    public class InitialPlayerSync : Packet
    {
        public NitroxId AssignedEscapePodId { get; set; }
        public List<EscapePodModel> EscapePodsData { get; set; }
        public List<EquippedItemData> EquippedItems { get; set; }
        public List<EquippedItemData> Modules { get; set; }
        public List<BasePiece> BasePieces { get; set; }
        public List<VehicleModel> Vehicles { get; set; }
        public List<ItemData> InventoryItems { get; set; }
        public List<ItemData> StorageSlotItems { get; set; }
        public List<NitroxTechType> UsedItems { get; set; }
        public List<string> QuickSlotsBinding { get; set; }
        public NitroxId PlayerGameObjectId { get; set; }
        public bool FirstTimeConnecting { get; set; }
        public InitialPDAData PDAData { get; set; }
        public InitialStoryGoalData StoryGoalData { get; set; }
        public IEnumerable<string> CompletedGoals { get; set; }
        public NitroxVector3 PlayerSpawnData { get; set; }
        public Optional<NitroxId> PlayerSubRootId { get; set; }
        public PlayerStatsData PlayerStatsData { get; set; }
        public List<InitialRemotePlayerData> RemotePlayerData { get; set; }
        public List<Entity> GlobalRootEntities { get; set; }
        public List<NitroxId> InitialSimulationOwnerships { get; set; }
        public ServerGameMode GameMode { get; set; }
        public Perms Permissions { get; set; }

        public InitialPlayerSync() { }

        public InitialPlayerSync(NitroxId playerGameObjectId,
            bool firstTimeConnecting,
            IEnumerable<EscapePodModel> escapePodsData,
            NitroxId assignedEscapePodId,
            IEnumerable<EquippedItemData> equipment,
            IEnumerable<EquippedItemData> modules,
            IEnumerable<BasePiece> basePieces,
            IEnumerable<VehicleModel> vehicles,
            IEnumerable<ItemData> inventoryItems,
            IEnumerable<ItemData> storageSlotItems,
            IEnumerable<NitroxTechType> usedItems,
            IEnumerable<string> quickSlotsBinding,
            InitialPDAData pdaData,
            InitialStoryGoalData storyGoalData,
            IEnumerable<string> completedGoals,
            NitroxVector3 playerSpawnData,
            Optional<NitroxId> playerSubRootId,
            PlayerStatsData playerStatsData,
            IEnumerable<InitialRemotePlayerData> remotePlayerData,
            IEnumerable<Entity> globalRootEntities,
            IEnumerable<NitroxId> initialSimulationOwnerships,
            ServerGameMode gameMode,
            Perms perms)
        {
            EscapePodsData = escapePodsData.ToList();
            AssignedEscapePodId = assignedEscapePodId;
            PlayerGameObjectId = playerGameObjectId;
            FirstTimeConnecting = firstTimeConnecting;
            EquippedItems = equipment.ToList();
            Modules = modules.ToList();
            BasePieces = basePieces.ToList();
            Vehicles = vehicles.ToList();
            InventoryItems = inventoryItems.ToList();
            StorageSlotItems = storageSlotItems.ToList();
            UsedItems = usedItems.ToList();
            QuickSlotsBinding = quickSlotsBinding.ToList();
            PDAData = pdaData;
            StoryGoalData = storyGoalData;
            CompletedGoals = completedGoals;
            PlayerSpawnData = playerSpawnData;
            PlayerSubRootId = playerSubRootId;
            PlayerStatsData = playerStatsData;
            RemotePlayerData = remotePlayerData.ToList();
            GlobalRootEntities = globalRootEntities.ToList();
            InitialSimulationOwnerships = initialSimulationOwnerships.ToList();
            GameMode = gameMode;
            Permissions = perms;
        }
    }
}
