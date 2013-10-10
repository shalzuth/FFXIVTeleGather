using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace FFXIVTeleGather
{
    public partial class Form1 : Form
    {
        Vector4 targetPos = new Vector4(0, 0, 0, 0);
        List<Zone> zones;
        Control basePos = new Control();
        int byteswritten;
        int bytesread;
        int basePointer;
        int charPointer;
        int charBase;
        IntPtr baseHandle;
        byte[] memory;
        public bool enterLoop = true;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);
        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        ProcessMemoryReader processMemoryReader = new ProcessMemoryReader();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadXml();
            UpdateZoneList();

            System.Diagnostics.Process[] myprocesses = System.Diagnostics.Process.GetProcessesByName("ffxiv");
            if (myprocesses.Length == 0)
                MessageBox.Show("No FFXIV instance found!", "No instance found!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            processMemoryReader.ReadProcess = myprocesses[0];
            processMemoryReader.OpenProcess();
            baseHandle = myprocesses[0].MainWindowHandle;
            basePointer = myprocesses[0].MainModule.BaseAddress.ToInt32();
            charPointer = basePointer + FFXIV.charOffset;
            memory = processMemoryReader.ReadProcessMemory((IntPtr)charPointer, 4, out bytesread);
            charBase = BitConverter.ToInt32(memory, 0);
            /*
            var timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000;
            timer.Start();
            */
        }
        void timer_Tick(object sender, EventArgs e)
        {
            Rect NotepadRect = new Rect();
            GetWindowRect(baseHandle, ref NotepadRect);
            this.Location = new Point(NotepadRect.Left, NotepadRect.Top);
        }
        private void ReloadXml()
        {
            XDocument xdoc = XDocument.Load("pos.xml");
            zones = new List<Zone>();
            //Run query
            var lv1s = from lv1 in xdoc.Descendants("Zone")
                       select new
                       {
                           Header = lv1.Attribute("ID").Value,
                           Children = lv1.Descendants("Pos")
                       };
            //Loop through results
            foreach (var lv1 in lv1s)
            {
                List<Position> positions = new List<Position>();
                foreach (var lv2 in lv1.Children)
                {
                    positions.Add(new Position(lv2.Attribute("name").Value, new Vector4((float)lv2.Attribute("x"), (float)lv2.Attribute("y"), (float)lv2.Attribute("z"), (float)lv2.Attribute("rot"))));
                }
                zones.Add(new Zone(lv1.Header, positions));
            }
        }
        private void SaveXml()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlWriter.Create("pos.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("World");
                foreach (Zone zone in zones)
                {
                    writer.WriteStartElement("Zone");
                    writer.WriteAttributeString("ID", zone.zoneName);
                    foreach (Position position in zone.positions)
                    {
                        writer.WriteStartElement("Pos");
                        writer.WriteAttributeString("name", position.name);
                        writer.WriteAttributeString("x", position.pos.x.ToString());
                        writer.WriteAttributeString("y", position.pos.y.ToString());
                        writer.WriteAttributeString("z", position.pos.z.ToString());
                        writer.WriteAttributeString("rot", position.pos.rot.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
        public void UpdatePositionList()
        {
            zonePositions.Items.Clear();
            zonePositions.Items.AddRange(zones[zones.IndexOf((Zone)zoneList.SelectedItem)].positions.ToArray());
            SaveXml();
        }
        public void UpdateZoneList()
        {
            zoneList.Items.Clear();
            zoneList.Items.AddRange(zones.ToArray());
            zoneList.SelectedIndex = 0;
        }
        public class FFXIV
        {
            public static int targetOffset = 0x00F87F80;
            public static int charOffset = 0x00F88EFC;
            //public static int charOffset = 0x00F8BA14;//0x00F88EFC also works????
            public static int statOffset = 0x30A4;
            public static int name = 0x30;
            public static int rot = 0xB0;
            public static int x = 0xA0;
            public static int y = 0xA8;
            public static int z = 0xA4;
            public static int zoomBase = 0x00F87F10;
            public static int zoomOffset = 0xE4;
            public static int zoneBase = 0x00F83B64;
            public static int zoneOffset1 = 0x7A4;
            public static int zoneOffset2 = 0x62C;
            public static int zoneOffset3 = 0x8;
            public static int zoneOffset4 = 0xC;
            public static int zoneOffset5 = 0xAE;
            public static int mapBase = 0x001CAAF8;
            public static int mapOffset1 = 0x70;
            public static int mapOffset2 = 0x4C;
            public static int mapOffsetX = 0x1F4;
            public static int mapOffsetY = 0x1F8;
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void zoomHack_Click(object sender, EventArgs e)
        {
            int zoomPointer = basePointer + FFXIV.zoomBase;
            memory = processMemoryReader.ReadProcessMemory((IntPtr)zoomPointer, 4, out bytesread);
            zoomPointer = BitConverter.ToInt32(memory, 0);
            zoomPointer += FFXIV.zoomOffset;
            memory = processMemoryReader.ReadProcessMemory((IntPtr)zoomPointer, 4, out bytesread);
            zoomPointer = BitConverter.ToInt32(memory, 0);
            memory = BitConverter.GetBytes(50f);
            processMemoryReader.WriteProcessMemory((IntPtr)zoomPointer, memory, out byteswritten);
        }

        private void zoneList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePositionList();
        }

        private void addWaypoint_Click(object sender, EventArgs e)
        {
            zones[zones.IndexOf((Zone)zoneList.SelectedItem)].positions.Add(new Position(waypointName.Text,GetCurrentPos()));
            UpdatePositionList();
        }
        private void deleteWaypoint_Click(object sender, EventArgs e)
        {
            zones[zones.IndexOf((Zone)zoneList.SelectedItem)].positions.Remove((Position)zonePositions.SelectedItem);
            UpdatePositionList();
        }

        private void teleportToWaypoint_Click(object sender, EventArgs e)
        {
            teleportTo(((Position)zonePositions.SelectedItem).pos);
        }
        private void teleportTo(Vector4 pos)
        {
            memory = BitConverter.GetBytes(pos.x);
            processMemoryReader.WriteProcessMemory((IntPtr)(charBase + FFXIV.x), memory, out byteswritten);
            memory = BitConverter.GetBytes(pos.y);
            processMemoryReader.WriteProcessMemory((IntPtr)(charBase + FFXIV.y), memory, out byteswritten);
            memory = BitConverter.GetBytes(pos.z);
            processMemoryReader.WriteProcessMemory((IntPtr)(charBase + FFXIV.z), memory, out byteswritten);
            memory = BitConverter.GetBytes(pos.rot);
            processMemoryReader.WriteProcessMemory((IntPtr)(charBase + FFXIV.rot), memory, out byteswritten);
        }
        private void hack()
        {
            return;
            SetForegroundWindow(baseHandle);


            int tarpointerbase = basePointer + FFXIV.targetOffset;
            enterLoop = true;
            while (enterLoop)
            {
                foreach (Position item in zonePositions.Items)
                {
                    int locpointerbase;
                    teleportTo(item.pos);
                    System.Threading.Thread.Sleep(100);
                    /*
                    PostMessage(ptr, 0x100, (IntPtr)(Keys.D), IntPtr.Zero);
                    Thread.Sleep(100);
                    PostMessage(ptr, 0x101, (IntPtr)(Keys.D), IntPtr.Zero);
                    PostMessage(ptr, 0x100, (IntPtr)(Keys.F12), IntPtr.Zero);
                    Thread.Sleep(10);
                    PostMessage(ptr, 0x101, (IntPtr)(Keys.F12), IntPtr.Zero);
                    PostMessage(ptr, 0x100, (IntPtr)(Keys.F12), IntPtr.Zero);
                    Thread.Sleep(10);
                    PostMessage(ptr, 0x101, (IntPtr)(Keys.F12), IntPtr.Zero);
                    PostMessage(ptr, 0x100, (IntPtr)(Keys.F12), IntPtr.Zero);
                    Thread.Sleep(10);
                    PostMessage(ptr, 0x101, (IntPtr)(Keys.F12), IntPtr.Zero);
                     * */
                    /*
                    SendKeys.SendWait("eeeeeee");
                    SendKeys.SendWait("{F12}");*/
                    memory = processMemoryReader.ReadProcessMemory((IntPtr)tarpointerbase, 4, out bytesread);
                    locpointerbase = BitConverter.ToInt32(memory, 0);
                    Vector4 targetPos = new Vector4(
                        BitConverter.ToSingle(processMemoryReader.ReadProcessMemory((IntPtr)(locpointerbase + FFXIV.x), 4, out bytesread), 0),
                        BitConverter.ToSingle(processMemoryReader.ReadProcessMemory((IntPtr)(locpointerbase + FFXIV.y), 4, out bytesread), 0),
                        BitConverter.ToSingle(processMemoryReader.ReadProcessMemory((IntPtr)(locpointerbase + FFXIV.z), 4, out bytesread), 0),
                        0);
                    Double dist = GetDistance(item.pos, targetPos);
                    if (dist > 0.1 && dist < 5)
                    {
                        String targetName = Encoding.UTF8.GetString(processMemoryReader.ReadProcessMemory((IntPtr)(locpointerbase + FFXIV.name), 24, out bytesread));
                        bool working = true;
                        Console.WriteLine(targetName);
                        if (!targetName.Contains("Mature") && !targetName.Contains("Lush"))
                        {
                            working = false;
                        }
                        while (working)
                        {
                            /*
                            PostMessage(ptr, 0x100, (IntPtr)(Keys.NumPad0), IntPtr.Zero);
                            Thread.Sleep(100);
                            PostMessage(ptr, 0x101, (IntPtr)(Keys.NumPad0), IntPtr.Zero);
                             * */
                            //SendKeys.SendWait("`");
                            System.Threading.Thread.Sleep(300);
                            memory = processMemoryReader.ReadProcessMemory((IntPtr)tarpointerbase, 4, out bytesread);
                            locpointerbase = BitConverter.ToInt32(memory, 0);
                            if (locpointerbase == 0)
                            {
                                working = false;
                            }

                        }
                        Thread.Sleep(500);
                    }

                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(hack);
            thread.Start();
            return;
        }
        Vector4 GetCurrentPos()
        {
            return new Vector4(
                BitConverter.ToSingle(processMemoryReader.ReadProcessMemory((IntPtr)(charBase + FFXIV.x), 4, out bytesread), 0),
                BitConverter.ToSingle(processMemoryReader.ReadProcessMemory((IntPtr)(charBase + FFXIV.y), 4, out bytesread), 0),
                BitConverter.ToSingle(processMemoryReader.ReadProcessMemory((IntPtr)(charBase + FFXIV.z), 4, out bytesread), 0),
                BitConverter.ToSingle(processMemoryReader.ReadProcessMemory((IntPtr)(charBase + FFXIV.rot), 4, out bytesread), 0));
        }
        Double GetDistance(Vector4 pos1, Vector4 pos2)
        {
            return Math.Sqrt(((pos1.x - pos2.x) * (pos1.x - pos2.x) + (pos1.y - pos2.y) * (pos1.y - pos2.y) + (pos1.z - pos2.z) * (pos1.z - pos2.z)));
        }
        Double GetAngle(Vector4 pos1, Vector4 pos2)
        {
            Double offset90 = Math.Atan2((pos1.y - pos2.y), (pos2.x - pos1.x));
            offset90 += Math.PI / 2;
            if (offset90 > Math.PI)
                offset90 -= 2 * Math.PI;
            return offset90;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            enterLoop = false;
        }
        private static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }
        private void goToTar(Vector4 pos)
        {
            Vector4 myPos = GetCurrentPos();
            double totalTurn = myPos.rot - GetAngle(myPos, pos);
            double totalDist = GetDistance(myPos, pos);
            Keys turning = Keys.Q;
            Keys running = Keys.Q;
            Console.WriteLine("turn:" + totalTurn);
            Console.WriteLine("dist:" + totalDist);
            mouse_event(1, -60, -30, 0, 0);
            Thread.Sleep(10);
            mouse_event(8, 0, 0, 0, 0);
            mouse_event(2, 0, 0, 0, 0);
            Thread.Sleep(10);
            while (enterLoop && totalDist > 1)
            {
                Thread.Sleep(1);
                myPos = GetCurrentPos();
                totalTurn = myPos.rot - GetAngle(myPos, pos);
                totalDist = GetDistance(myPos, pos);

                if ((Math.Abs(totalTurn) > 0.05))
                {
                    Keys dir = Keys.D;
                    if ((totalTurn > -Math.PI && totalTurn < 0) || totalTurn > Math.PI)
                        dir = Keys.A;
                    if (dir == Keys.A)
                    {
                        mouse_event(1, -6, 0, 0, 0);
                    }
                    else
                    {
                        mouse_event(1, 6, 0, 0, 0);
                    }
                }
            }
            mouse_event(4, 0, 0, 0, 0);
            mouse_event(16, 0, 0, 0, 0);
            /*while (enterLoop && totalDist > 1)
            {
                Thread.Sleep(100);
                myPos = GetCurrentPos();
                totalTurn = myPos.rot - GetAngle(myPos, pos);
                totalDist = GetDistance(myPos, pos);
                //Console.WriteLine("turn:" + totalTurn);
                //Console.WriteLine("dist:" + totalDist);
                
                if ((Math.Abs(totalTurn) > 0.01))
                {
                    Keys dir = Keys.D;
                    if ((totalTurn > -Math.PI && totalTurn < 0) || totalTurn > Math.PI)
                        dir = Keys.A;
                    if (turning == Keys.Q)
                    {
                        turning = dir;
                        SendMessage(baseHandle, 0x100, (IntPtr)turning, IntPtr.Zero);
                    }
                    if (turning != dir)
                    {
                        SendMessage(baseHandle, 0x101, (IntPtr)turning, IntPtr.Zero);
                        turning = dir;
                        Thread.Sleep(10);
                        SendMessage(baseHandle, 0x100, (IntPtr)turning, IntPtr.Zero);
                    }

                    //Console.WriteLine("turn:" + totalTurn + "\t going:" + dir);
                }
                else
                {
                    if (turning != Keys.Q)
                    {
                        SendMessage(baseHandle, 0x100, (IntPtr)turning, IntPtr.Zero);
                        turning = Keys.Q;
                    }
                }
                if (totalDist > 1)
                {
                    if (running == Keys.Q)
                    {
                        running = Keys.W;
                        SendMessage(baseHandle, 0x100, (IntPtr)running, IntPtr.Zero);
                    }
                }
                else
                {
                    if (running != Keys.Q)
                    {
                        SendMessage(baseHandle, 0x101, (IntPtr)running, IntPtr.Zero);
                        running = Keys.Q;
                    }
                }
            }
            if (turning != Keys.Q)
            {
                SendMessage(baseHandle, 0x101, (IntPtr)turning, IntPtr.Zero);
            }
            if (running != Keys.Q)
            {
                SendMessage(baseHandle, 0x101, (IntPtr)running, IntPtr.Zero);
            }*/
            Thread.Sleep(100);
        }
        private void gotoPos()
        {
            int counter = 0;
            foreach (Position item in zonePositions.Items)
            {
                if (enterLoop)
                {
                    goToTar(item.pos);
                    Console.WriteLine("count:" + counter++);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            enterLoop = true;
            SetForegroundWindow(baseHandle);
            Thread.Sleep(500);
            Thread gotoThread = new Thread(gotoPos);
            gotoThread.Name = "goto";
            gotoThread.Start();
        }
    }
}
