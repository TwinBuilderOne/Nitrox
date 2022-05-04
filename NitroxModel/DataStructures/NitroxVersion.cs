using System;

namespace NitroxModel.DataStructures;

// Serializable version of System.Version
public struct NitroxVersion : IComparable<NitroxVersion>
{
    public int Major { get; set; }

    public int Minor { get; set; }

    public NitroxVersion(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }

    public int CompareTo(NitroxVersion other)
    {
        if (Major != other.Major)
        {
            if (Major > other.Major)
            {
                return 1;
            }

            return -1;
        }

        if (Minor != other.Minor)
        {
            if (Minor > other.Minor)
            {
                return 1;
            }

            return -1;
        }

        return 0;
    }

    public override string ToString() => $"{Major}.{Minor}";
}
