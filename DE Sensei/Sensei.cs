using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using DevComponents.DotNetBar;
using Ionic.Zip;
using Microsoft.Win32;
using Steamworks;

namespace DE_Sensei
{
    
    public partial class Sensei : OfficeForm
    {

        perfCLASS pf = new perfCLASS();
        public string dePATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Games\Age of Empires 2 DE";
        public Dictionary<string, string> ModMenu = new Dictionary<string, string>();
        public string saveDirectoryPath = "";
        private bool done = false;
        public Sensei()
        {

            InitializeComponent();

            if (!pf.senseiEXISTS())
                pf.senseiBORN();

            if(!pf.KidsHERE())
                pf.senseiBORN();
            
            RefreshMods();
            pf.RetSETTINGS("Splash Screen Pixelate", splashswitch);
            pf.RetSETTINGS2("Shrink", shrinktrack, shrinklbl);
            pf.RetSETTINGS("Skip Intro", introswitch);
            pf.RetSETTINGS("Disable Effects", effectswitch);
            pf.RetSETTINGS3("Performance", perfcb);
            

            //Decompress(new FileInfo(@"C:\Users\shock\Games\Age of Empires 2 DE\76561198079200175\profile\Player.nfp"));

        }
        
        public string SaveDirectoryPath()
        {
            if (this.saveDirectoryPath == "")
            {
                uint size = SteamApps.GetAppInstallDir((AppId_t)813780, out this.saveDirectoryPath, 500u);

                if (size <= 0)
                {

                }
                else
                {

                }
            }

            return this.saveDirectoryPath;
        }
        //public static void Decompress(FileInfo fileToDecompress)
        //{
        //    using (FileStream originalFileStream = fileToDecompress.OpenRead())
        //    {
        //        string currentFileName = fileToDecompress.FullName;
        //        string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

        //        using (FileStream decompressedFileStream = File.Create(newFileName))
        //        {
        //            using (DeflateStream decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
        //            {
        //                decompressionStream.CopyTo(decompressedFileStream);
        //                MessageBoxEx.Show("Decompressed: {0}", fileToDecompress.Name);
        //            }
        //        }
        //    }
        //}
        private void RemoveDirectories(string strpath)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                if (Directory.Exists(strpath))
                {
                    
                        DirectoryInfo dirInfo = new DirectoryInfo(strpath);
                    var files = dirInfo.GetFiles();
                    //I assume your code is inside a Form, else you need a control to do this invocation;
                    this.BeginInvoke(new Action(() =>
                    {
                        pBAR.Minimum = 0;
                        pBAR.Value = 0;
                        pBAR.Maximum = files.Length;
                        pBAR.Step = 1;
                    }));

                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                        this.BeginInvoke(new Action(() => pBAR.PerformStep())); //I assume your code is inside a Form, else you need a control to do this invocation;

                    }

                    var dirs = dirInfo.GetDirectories();

                    this.BeginInvoke(new Action(() =>
                    {
                        pBAR.Value = 0;
                        pBAR.Maximum = dirs.Length;
                    }));

                    foreach (DirectoryInfo dir in dirs)
                    {
                        
                        dir.Delete(true);

                        this.BeginInvoke(new Action(() => pBAR.PerformStep())); //I assume your code is inside a Form, else you need a control to do this invocation;
                    }
                  
            }
            }, null);

        }
        private void Sensei_Load(object sender, EventArgs e)
        {
            int _sw = this.Width;
            int _sh = this.Height;
            this.MinimumSize = new Size(_sw, _sh);
            this.MaximumSize = new Size(_sw, _sh);
            //Task.Factory.StartNew(path => Directory.Delete((string)path, true), @"C:\Users\shock\Desktop\shithere");
        }

        private async void perfcb_SelectedValueChanged(object sender, EventArgs e)
        {
            if(perfcb.SelectedItem.ToString() == "High Performance" && perfcb.Focus())
            {
                File.WriteAllText(System.IO.Path.GetTempPath() + "perf.reg", Properties.Resources.low);
                Task<int> ImpRegistry = pf.importRegistry(System.IO.Path.GetTempPath() + "perf.reg");
                int resultimp = await ImpRegistry;
                if (resultimp == 1)
                {
                    pf.SenseiREG("Performance", 1, Microsoft.Win32.RegistryValueKind.DWord);
                    MessageBoxEx.Show("Graphics Settings are set to LOW.", "High Performance Activated!");
                }
            }
            else if (perfcb.SelectedItem.ToString() == "Default" && perfcb.Focus())
            {
                File.WriteAllText(System.IO.Path.GetTempPath() + "perf.reg", Properties.Resources.def);
                Task<int> ImpRegistry = pf.importRegistry(System.IO.Path.GetTempPath() + "perf.reg");
                int resultimp = await ImpRegistry;
                if (resultimp == 1)
                {
                    pf.SenseiREG("Performance", 0, Microsoft.Win32.RegistryValueKind.DWord);
                    MessageBoxEx.Show("Graphics Settings are set back to DEFAULT.", "Default Activated!");
                }
            }

        }

        private void resocb_SelectedValueChanged(object sender, EventArgs e)
        {
          
        }

        private void effectswitch_ValueChanged(object sender, EventArgs e)
        {
            if(effectswitch.ValueObject.ToString() == "Y")
            {
                efconfig.Enabled = true;
                if(effectswitch.Focus())
                {
                //Disabling cool destructions, particles
                pf.RegEdit("Cool Destructions", 0, Microsoft.Win32.RegistryValueKind.DWord);
                pf.RegEdit("Particles", 0, Microsoft.Win32.RegistryValueKind.DWord);
                pf.SenseiREG("Disable Effects", 1, Microsoft.Win32.RegistryValueKind.DWord);
                
                DialogResult dres = MessageBoxEx.Show("Cool Destructions & Particles are Disabled.\n Press OK to Disable/Enable Other Hidden Game Effects & Animations.", "Success!");
                if(dres == DialogResult.OK)
                    {
                        //More Effects to disable window
                        Main mn = new Main();
                        mn.GetDEPath = SaveDirectoryPath();
                        mn.GetGames = dePATH;
                        mn.Show();
                    }
              
                }
            }
            else if (effectswitch.ValueObject.ToString() == "N" && effectswitch.Focus())
            {
                //Enabling cool destructions, particles
                pf.RegEdit("Cool Destructions", 1, Microsoft.Win32.RegistryValueKind.DWord);
                pf.RegEdit("Particles", 0, Microsoft.Win32.RegistryValueKind.DWord);
                pf.SenseiREG("Disable Effects", 0, Microsoft.Win32.RegistryValueKind.DWord);
                MessageBoxEx.Show("Cool Destructions & Particles are Enabled.", "Success!");
            }
        }
        public bool PSubmiter(ListBoxItem rec, string title)
        {
            //ListBoxItem rec = new ListBoxItem();
            rec.Text = title;
            rec.Image = DE_Sensei.Properties.Resources.cloud;
            listsubmitee.Items.Add(rec);
            return true;
        }
        public void RefreshMods()
        {
            ModMenu.Clear();
            listsubmitee.Items.Clear();
            if (!Directory.Exists(dePATH))
            {
                MessageBoxEx.Show("Your AoE2 Profile Folder Do Not Exist! Try to Run AoE2 DE Then Try Again!", "Alert!");
                return;
            }
            var subdirectoryEntries = new DirectoryInfo(dePATH).GetDirectories("*", SearchOption.AllDirectories).OrderByDescending(x => x.LastWriteTime);
            if (subdirectoryEntries.Count() == 0)
            {
                MessageBoxEx.Show("Your AoE2 Profile Folder Do Not Exist! Try to Run AoE2 DE Then Try Again!", "Alert!");
                return;
            }
            foreach (DirectoryInfo subdirectory in subdirectoryEntries)
            {

                if (pf.IsDigitsOnly(subdirectory.FullName.Replace(dePATH + "\\", "")) && subdirectory.FullName.Length > 4 && subdirectory.FullName.Replace(dePATH + "\\", "") != "0")
                {
                    var allmods = new DirectoryInfo(subdirectory.FullName + @"\mods\subscribed").GetDirectories("*", SearchOption.TopDirectoryOnly).OrderByDescending(x => x.LastWriteTime);
                    foreach (DirectoryInfo dirmod in allmods)
                    {

                        //Store mods in dictionary to save resources
                        if (!ModMenu.ContainsKey(Regex.Replace(dirmod.Name, @"[0-9]+_", "")))
                            ModMenu.Add(Regex.Replace(dirmod.Name, @"[0-9]+_", ""), dirmod.FullName);

                        ListBoxItem rec = new ListBoxItem();
                        PSubmiter(rec, Regex.Replace(dirmod.Name, @"[0-9]+_", ""));

                    }



                }
            }
            //set mod counter
            modcount.Text = listsubmitee.Items.Count.ToString();
            itemselec.Text = listsubmitee.SelectedItems.Count.ToString();
        }
        private void modswitch_ValueChanged(object sender, EventArgs e)
        {
            if (modswitch.ValueObject.ToString() == "Y" && modswitch.Focus())
            {
                RefreshMods();
                pf.SenseiREG("Mods Manager", 1, Microsoft.Win32.RegistryValueKind.DWord);

            }
            else if(modswitch.ValueObject.ToString() == "N" && modswitch.Focus())
            {
                listsubmitee.Items.Clear();
                //clear mod counter
                modcount.Text = "0";
                itemselec.Text = "0";
                pf.SenseiREG("Mods Manager", 0, Microsoft.Win32.RegistryValueKind.DWord);
            }
        }

        private void modslist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public async Task<int> xCopy(string SourcePath, string DestinationPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files & Replaces any files with the same name
            List<string> exts = new List<string> { "thumbnail.png", "thumbnail.jpg", "info.json", "_preview-icon.png", "_preview-icon.jpg" };
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                SearchOption.AllDirectories))
            {
                if(!newPath.Contains(exts[0]) && !newPath.Contains(exts[1]) && !newPath.Contains(exts[2]))
                {
                    using (Stream source = File.OpenRead(newPath))
                    {
                        using (Stream destination = File.Create(newPath.Replace(SourcePath, DestinationPath)))
                        {
                            await source.CopyToAsync(destination);
                            pBAR.Value++;
                        }
                    }
                }
                //File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
            }
            return 0;
               
        }
        private async void offinstall_Click(object sender, EventArgs e)
        {
            int filesCOUNT = 0;
            var miMOD = listsubmitee.SelectedItems.ToList();
            foreach (var item in listsubmitee.SelectedItems)
            {
                if (ModMenu.ContainsKey(item.Text))
                {
                    string mpath;
                    if (ModMenu.TryGetValue(item.Text, out mpath))
                        filesCOUNT = Directory.GetFiles(mpath,"*.*").Count();
                }
             }
            //Set Progress
            pBAR.Maximum = filesCOUNT;

            foreach (var item in listsubmitee.SelectedItems)
            {
                if (ModMenu.ContainsKey(item.Text))
                {
                    string mpath;
                    if (ModMenu.TryGetValue(item.Text, out mpath))
                        await xCopy(mpath, saveDirectoryPath);
                    //await pf.CopyFileAsync(mpath, saveDirectoryPath, pBAR);



                    
                }
                
                //xCopy(item.Name, saveDirectoryPath);
            }
            pBAR.Value = pBAR.Maximum;
            pBAR.Value = 0;
            DialogResult dr = MessageBoxEx.Show("Installation of Following Mods is Successful: \n" + miMOD.Aggregate("", (current, next) =>  current + @"[x]" + next + "\n") + "\n Would You Like To Delete Online Ones To Speed Up AoE2 DE?", "Success!",MessageBoxButtons.YesNo);
            if(dr == DialogResult.Yes)
            {
                deleteit.PerformClick();
            }
            else { }
        }

        private void listsubmitee_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemselec.Text = listsubmitee.SelectedItems.Count.ToString();
        }

        private void listsubmitee_ItemClick(object sender, EventArgs e)
        {

        }

        private void listsubmitee_ItemAdded(object sender, EventArgs e)
        {
            modcount.Text = listsubmitee.Items.Count.ToString();
        }

        private async void TickWorks_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!SteamAPI.Init())
                {
                    if (SteamAPI.IsSteamRunning())
                    {
                        steamLBL.ForeColor = Color.DarkBlue;
                        steamLBL.Text = "Steam Running..";
                        steamSTATUS.Image = DE_Sensei.Properties.Resources.steam_red;
                    }
                    else
                    {
                        steamLBL.ForeColor = Color.Gold;
                        await Task.Delay(200);
                        steamLBL.Text = "Steam OFF";
                        await Task.Delay(100);
                        steamLBL.ForeColor = Color.Maroon;
                        steamSTATUS.Image = DE_Sensei.Properties.Resources.steam_red;
                    }

                    //SteamUtils.GetAppID();

                }
                else
                {
                    TickWorks.Stop();
                    steamLBL.ForeColor = Color.ForestGreen;
                    steamLBL.Text = "Steam ON";
                    steamSTATUS.Image = DE_Sensei.Properties.Resources.steam_green;
                    await Task.Delay(200);

                    bool IsDE = SteamApps.BIsAppInstalled(new AppId_t(813780));
                    if (!IsDE)
                    {
                        MessageBoxEx.Show("Please Buy or Install Age of Empires II: Definitive Edition before you proceed!", "Game Missing");
                        TickWorks.Stop();
                    }
                    //Retrieve game folder
                    
                    SaveDirectoryPath();
                    Thread.Sleep(100);
                    //Enable Sensei features
                    //perfcb.Enabled = true;
                    //shrinktrack.Enabled = true;
                    //splashswitch.Enabled = true;
                    //modswitch.Enabled = true;
                    //introswitch.Enabled = true;
                    //effectswitch.Enabled = true;

                    foreach (Control ctrl in flowLayoutPanel1.Controls)
                    {
                        ctrl.Enabled = true;
                    }
                    //Retrieve settings
                    pf.CheckLayers(saveDirectoryPath + @"\AoE2DE_s.exe");

                    pf.RetSETTINGS4(saveDirectoryPath + @"\AoE2DE_s.exe", runas, "RUNASADMIN");
                    pf.RetSETTINGS4(saveDirectoryPath + @"\AoE2DE_s.exe", screenopt, "DISABLEDXMAXIMIZEDWINDOWEDMODE");
                    pf.RetSETTINGS4(saveDirectoryPath + @"\AoE2DE_s.exe", dpiscal, "HIGHDPIAWARE");
                    //Stop ticker
                    
                }
            }
            catch (SystemException)
            {
                steamLBL.ForeColor = Color.Black;
                steamLBL.Text = "Steam OFF";
                steamSTATUS.Image = DE_Sensei.Properties.Resources.steam_red;

            }
        }

        private void selectmods_CheckedChanged(object sender, EventArgs e)
        {
            if(selectmods.Checked == true && listsubmitee.Items.Count > 0)
            {
                for (int i = 0; i < listsubmitee.Items.Count; i++)
                {
                    listsubmitee.SetSelected(i, true);
                }
                
                //foreach(var mod in listsubmitee.Items)
                //    mod.SetSelected
            }
            else if(selectmods.Checked == false && listsubmitee.Items.Count > 0)
            {
                for (int i = 0; i < listsubmitee.Items.Count; i++)
                {
                    listsubmitee.SetSelected(i, false);
                }
            }
        }

        private void browsemod_Click(object sender, EventArgs e)
        {
            if (listsubmitee.SelectedItems.Count == 1)
            {
                string mpath;
                if (ModMenu.TryGetValue(listsubmitee.SelectedItem.ToString(), out mpath))
                {
                    if(Directory.Exists(mpath))
                        Process.Start(mpath);
                }
                    
            }
            else if(listsubmitee.SelectedItems.Count > 1)
            {
                MessageBoxEx.Show("Please Select Only One Mod! Multiple selections not allowed.", "Multiple Selections Detected!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void efconfig_Click(object sender, EventArgs e)
        {
            
            Main mn = new Main();
            mn.GetDEPath = SaveDirectoryPath();
            mn.GetGames = dePATH;
            mn.Show();
        }

        private void efconfig_MouseHover(object sender, EventArgs e)
        {
            efconfig.Image = Properties.Resources.gear2;
        }

        private void efconfig_MouseLeave(object sender, EventArgs e)
        {
            efconfig.Image = Properties.Resources.gear;
        }

        private void introswitch_ValueChanged(object sender, EventArgs e)
        {
            if (introswitch.ValueObject.ToString() == "Y" && introswitch.Focus())
            {
                if (File.Exists(saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv"))
                    File.Move(saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv", saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv.old");
                    
                if (File.Exists(saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv"))
                    File.Move(saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv", saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv.old");

                pf.SenseiREG("Skip Intro", 1, Microsoft.Win32.RegistryValueKind.DWord);
                MessageBoxEx.Show("Intro Videos Successfully Disabled!", "Intro Disabled!");

            }
            else if (introswitch.ValueObject.ToString() == "N" && introswitch.Focus())
            {
                if (File.Exists(saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv.old"))
                    File.Move(saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv.old", saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv");

                if (File.Exists(saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv.old"))
                    File.Move(saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv.old", saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv");

                pf.SenseiREG("Skip Intro", 0, Microsoft.Win32.RegistryValueKind.DWord);
                MessageBoxEx.Show("Intro Videos Enabled!", "Intro Enabled!");
            }
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        static string ReadableFileSize(double size, int unit = 0)
        {
            string[] units = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

            while (size >= 1024)
            {
                size /= 1024;
                ++unit;
            }

            return String.Format("{0:0.#} {1}.", size, units[unit]);
        }
        
        private void splashswitch_ValueChanged(object sender, EventArgs e)
        {
            if (splashswitch.ValueObject.ToString() == "Y" && splashswitch.Focus())
            {
                if (File.Exists(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp"))
                {
                    string sizeSPLA;
                    if (!File.Exists(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp.old"))
                        File.Copy(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp", saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp.old", true);

                    if (new FileInfo(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp").Length > 500000)
                    {
                        sizeSPLA = ReadableFileSize(new FileInfo(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp").Length);
                        File.WriteAllBytes(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp", ImageToByte(Properties.Resources.loading_slash_sml));
                        pf.SenseiREG("Splash Screen Pixelate",1,Microsoft.Win32.RegistryValueKind.DWord);
                        MessageBoxEx.Show("Success! Splash Screen is Pixelated to 7 KB - From " + sizeSPLA??"900 KB.", "Results");
                    }
                    else
                    {
                        sizeSPLA = ReadableFileSize(new FileInfo(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp").Length);
                        pf.SenseiREG("Splash Screen Pixelate", 1, Microsoft.Win32.RegistryValueKind.DWord);
                        MessageBoxEx.Show("Success! Splash Screen is Pixelated to 7 KB - From " + sizeSPLA ?? "900 KB.", "Results");
                    }

                }
            }
            else if (splashswitch.ValueObject.ToString() == "N" && splashswitch.Focus())
            {
                if (File.Exists(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp.old"))
                {
                    File.WriteAllBytes(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp", File.ReadAllBytes(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp.old"));
                    pf.SenseiREG("Splash Screen Pixelate", 0, Microsoft.Win32.RegistryValueKind.DWord);
                    MessageBoxEx.Show("Success! Splash Screen Restored.", "Results");
                }

            }
            
            
        }

        private void perfcb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void resocb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rangeSlider1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void shrinktrack_ValueChanged(object sender, EventArgs e)
        {
            if(shrinktrack.Focus())
            {
                shrinklbl.Text = shrinktrack.Value.ToString() + "%";
                Task.Delay(1000);
                pf.SenseiREG("Shrink", shrinktrack.Value, Microsoft.Win32.RegistryValueKind.DWord);
            }
            
        }
        public void DeleteDirectory(string target_dir)
        {
            try
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);
                pBAR.Maximum += files.Count() + dirs.Count();
                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                    pBAR.Value++;
                }

                foreach (string dir in dirs)
                {
                    Directory.Delete(dir, true);
                    pBAR.Value++;
                }
                Directory.Delete(target_dir, true);
            }
            catch(System.IO.DirectoryNotFoundException)
            {
                
            }
            

        }
        private void deleteit_Click(object sender, EventArgs e)
        {
            if(listsubmitee.SelectedItems.Count>0)
            {

            
            DialogResult dr = MessageBoxEx.Show("Confirm Deletion of Mod(s).", "Alert", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    {
                pBAR.Maximum = 0;
                pBAR.Value = 0;
                //Directory.Delete(target_dir, false);
                foreach (var item in listsubmitee.SelectedItems)
                {
                    if (ModMenu.ContainsKey(item.Text))
                    {
                        string mpath;
                        if (ModMenu.TryGetValue(item.Text, out mpath))
                        {
                            DeleteDirectory(mpath);
                            //filesCOUNT = Directory.GetFiles(mpath, "*").Count();
                            //dirsCOUNT = Directory.GetDirectories(mpath, "*").Count();
                        }

                    }
                }
                RefreshMods();
                pBAR.Value = 0;
            }
                else return;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void refreshmods_Click(object sender, EventArgs e)
        {
            if((string)modswitch.ValueObject == "Y")
                RefreshMods();
        }
        private static string FormatByteSize(double bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "TB" };
            int index = 0;
            do { bytes /= 1024; index++; }
            while (bytes >= 1024);
            return String.Format("{0:0.00} {1}", bytes, Suffix[index]);

        }

        public void SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_Started)
            {
                //KryptonMessageBox.Show("Begin Saving: " + e.ArchiveName);
            }
            else if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry || e.EventType == ZipProgressEventType.Extracting_BeforeExtractAll)
            {
                //labelCompressionStatus.Text = "Writing: " + e.CurrentEntry.FileName + " (" + (e.EntriesSaved + 1) + "/" + e.EntriesTotal + ")";
                //labelFilename.Text = "Filename:" + e.CurrentEntry.LocalFileName;

                pBAR2.Maximum = e.EntriesTotal;
                pBAR2.Value = e.EntriesSaved + 1;
            }
            else if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead || e.EventType == ZipProgressEventType.Extracting_AfterExtractEntry)
            {
                pBAR.Value = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer);
            }
            else if (e.EventType == ZipProgressEventType.Saving_Completed || e.EventType == ZipProgressEventType.Extracting_AfterExtractAll)
            {
                //KryptonMessageBox.Show("Done: " + e.ArchiveName);
                pBAR.Value = 0;
                pBAR2.Value = 0;
            }
        }
        private void exportit_Click(object sender, EventArgs e)
        {

            if (listsubmitee.SelectedIndex >= 0 || listsubmitee.CheckedItems.Count >= 0)
            {
                // Show the FolderBrowserDialog.
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Archive Zip|*.Zip";
                saveFileDialog1.Title = "Save Zip File To";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    String ZipFileToCreate = saveFileDialog1.FileName;

                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                        zip.SaveProgress += SaveProgress;

                        zip.StatusMessageTextWriter = System.Console.Out;
                        foreach (var mod in listsubmitee.SelectedItems)
                        {
                            if (ModMenu.ContainsKey(mod.ToString()))
                            {
                                string mpath;
                                if (ModMenu.TryGetValue(mod.ToString(), out mpath))
                                    zip.AddDirectory(mpath, mod.ToString());
                            }
                            
                        }

                        zip.Save(ZipFileToCreate);
                    }

                    FileInfo sizez = new FileInfo(saveFileDialog1.FileName);
                    KryptonMessageBox.Show("Mods Size: " + FormatByteSize(sizez.Length), "Success");
                    //end dialog
                }

                else
                {

                }
            }
        }

        private void runas_CheckedChanged(object sender, EventArgs e)
        {

            string EXEinfo = pf.EXEread(saveDirectoryPath + @"\AoE2DE_s.exe");
            string EXEpath = saveDirectoryPath + @"\AoE2DE_s.exe";
            if (runas.Checked && !EXEinfo.StartsWith(@"~"))
                pf.EXEsettings(EXEpath, EXEinfo + @"~ RUNASADMIN", Microsoft.Win32.RegistryValueKind.String);
            else if (runas.Checked && EXEinfo.StartsWith(@"~") && !EXEinfo.Contains("RUNASADMIN"))
                pf.EXEsettings(EXEpath, EXEinfo + @" RUNASADMIN", Microsoft.Win32.RegistryValueKind.String);
            else if(!runas.Checked && !EXEinfo.StartsWith(@"~ RUNASADMIN"))
                pf.EXEsettings(EXEpath, EXEinfo.Replace(@" RUNASADMIN",""), Microsoft.Win32.RegistryValueKind.String);
            else if (!runas.Checked && EXEinfo.StartsWith(@"~ RUNASADMIN"))
                pf.EXEsettings(EXEpath, @"~" + EXEinfo.Replace(@"~ RUNASADMIN", ""), Microsoft.Win32.RegistryValueKind.String);


        }
        
        private void screenopt_CheckedChanged(object sender, EventArgs e)
        {
            string EXEinfo = pf.EXEread(saveDirectoryPath + @"\AoE2DE_s.exe");
            string EXEpath = saveDirectoryPath + @"\AoE2DE_s.exe";
            if (runas.Checked && !EXEinfo.StartsWith(@"~"))
                pf.EXEsettings(EXEpath, EXEinfo + @"~ DISABLEDXMAXIMIZEDWINDOWEDMODE", Microsoft.Win32.RegistryValueKind.String);
            else if (runas.Checked && EXEinfo.StartsWith(@"~") && !EXEinfo.Contains("DISABLEDXMAXIMIZEDWINDOWEDMODE"))
                pf.EXEsettings(EXEpath, EXEinfo + @" DISABLEDXMAXIMIZEDWINDOWEDMODE", Microsoft.Win32.RegistryValueKind.String);
            else if (!runas.Checked && !EXEinfo.StartsWith(@"~ DISABLEDXMAXIMIZEDWINDOWEDMODE"))
                pf.EXEsettings(EXEpath, EXEinfo.Replace(@" DISABLEDXMAXIMIZEDWINDOWEDMODE", ""), Microsoft.Win32.RegistryValueKind.String);
            else if (!runas.Checked && EXEinfo.StartsWith(@"~ DISABLEDXMAXIMIZEDWINDOWEDMODE"))
                pf.EXEsettings(EXEpath, @"~" + EXEinfo.Replace(@"~ DISABLEDXMAXIMIZEDWINDOWEDMODE", ""), Microsoft.Win32.RegistryValueKind.String);
        }

        private void dpiscal_CheckedChanged(object sender, EventArgs e)
        {
            string EXEinfo = pf.EXEread(saveDirectoryPath + @"\AoE2DE_s.exe");
            string EXEpath = saveDirectoryPath + @"\AoE2DE_s.exe";
            if (runas.Checked && !EXEinfo.StartsWith(@"~"))
                pf.EXEsettings(EXEpath, EXEinfo + @"~ HIGHDPIAWARE", Microsoft.Win32.RegistryValueKind.String);
            else if (runas.Checked && EXEinfo.StartsWith(@"~") && !EXEinfo.Contains("HIGHDPIAWARE"))
                pf.EXEsettings(EXEpath, EXEinfo + @" HIGHDPIAWARE", Microsoft.Win32.RegistryValueKind.String);
            else if (!runas.Checked && !EXEinfo.StartsWith(@"~ HIGHDPIAWARE"))
                pf.EXEsettings(EXEpath, EXEinfo.Replace(@" HIGHDPIAWARE", ""), Microsoft.Win32.RegistryValueKind.String);
            else if (!runas.Checked && EXEinfo.StartsWith(@"~ HIGHDPIAWARE"))
                pf.EXEsettings(EXEpath, @"~" + EXEinfo.Replace(@"~ HIGHDPIAWARE", ""), Microsoft.Win32.RegistryValueKind.String);
        }
    }
}
