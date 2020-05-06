using System.Runtime.InteropServices;

namespace VTCManager_1._0._0
{
    public class MemoryInfo
    {
        [StructLayout(LayoutKind.Sequential)]
        internal class MEMORYSTATUS
        {
            internal int length;
            internal int memoryLoad;
            internal uint totalPhys;
            internal uint availPhys;
            internal uint totalPageFile;
            internal uint availPageFile;
            internal uint totalVirtual;
            internal uint availVirtual;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GlobalMemoryStatus(MEMORYSTATUS buffer);

        MEMORYSTATUS memorystatus = new MEMORYSTATUS();

        public MemoryInfo()
        {
        }

        public MemoryInfo(bool Update)
        {
            if (Update)
                this.Update();
        }

        public bool Update()
        {
            return GlobalMemoryStatus(memorystatus);
        }

        public int MemoryLoad
        {
            get
            {
                return memorystatus.memoryLoad;
            }
        }

        public uint TotalPhys
        {
            get
            {
                return memorystatus.totalPhys;
            }
        }

        public uint AvailPhys
        {
            get
            {
                return memorystatus.availPhys;
            }
        }

        public uint TotalPageFile
        {
            get
            {
                return memorystatus.totalPageFile;
            }
        }

        public uint AvailPageFile
        {
            get
            {
                return memorystatus.availPageFile;
            }
        }

        public uint TotalVirtual
        {
            get
            {
                return memorystatus.totalVirtual;
            }
        }

        public uint AvailVirtual
        {
            get
            {
                return memorystatus.availVirtual;
            }
        }
    }
}
