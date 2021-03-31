using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE_Sensei
{
    
    public partial class Main : OfficeForm
    {
       

        public string GetDEPath { get; set; }
        public string GetGames { get; set; }

        perfCLASS pf = new perfCLASS();
        public CheckBox[] ChB;
        public CheckBox box;
        private bool _dirEXISTS;
        public Main()
        {
            InitializeComponent();
            
        }

        private async void Main_Load(object sender, System.EventArgs e)
        {
            
            //Initiate Effects
            //var getf = pf._Effects;
            await Task.Run(() => { 
            pf._allEFFECTS.Clear();
            if (Directory.Exists(GetDEPath + @"\resources\_common\particles\textures\atlases\"))
            {
                 var getf = Directory.GetFiles(GetDEPath + @"\resources\_common\particles\textures\atlases\", "*.json").ToList();
                foreach (string s in getf)
                    pf._allEFFECTS.Add(Path.GetFileName(s));
                var finalEF = pf._allEFFECTS;

                for (int i = 0; i < finalEF.Count; i++)
                {
                    box = new CheckBox();
                    box.Tag = i.ToString();
                    box.Name = i.ToString();
                    box.AutoSize = true;
                    box.Text = finalEF[i][0].ToString().ToUpper() + finalEF[i].Substring(1).Replace(".json", "").Replace("_", " ");
                    //Get Status
                    if (new FileInfo(GetDEPath + @"\resources\_common\particles\textures\atlases\" + finalEF[i]).Length == 0)
                        box.Checked = true;



                        if (!eflayer.Controls.ContainsKey(i.ToString()))
                        {
                            eflayer.Invoke(new MethodInvoker(delegate
                            {
                                eflayer.Controls.Add(box);
                            }));
                        }
                        

                }
                    //Backup
                    progCOPY.Invoke(new MethodInvoker(delegate
                    {
                        progCOPY.Maximum = pf._allEFFECTS.Count;
                    }));
                    progCOPY.Invoke(new MethodInvoker(delegate
                    {
                        progCOPY.Maximum = pf._allEFFECTS.Count;
                    }));
                   
                    _dirEXISTS = true;
                
            }
            });
            if(_dirEXISTS)
                await backupFILES();
            var reloadDEL = this.Controls.OfType<PictureBox>().FirstOrDefault(l => l.Name == "reloadGIF");
            if (reloadDEL != null)
                this.Controls.Remove(reloadDEL);
            reloadGIF.Visible = false;
        }
        
        public async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            using (Stream source = File.OpenRead(sourcePath))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                    progCOPY.Value++;
                }
            }
            
        }
        public async Task EmptyFileAsync(string destinationPath)
        {
           
                using (StreamWriter stream = new StreamWriter(destinationPath))
                {
                    await stream.WriteAsync(String.Empty);
                    progCOPY.Value++;
                }

        }
        private void selectcbs_CheckedChanged(object sender, System.EventArgs e)
        {
            if(selectcbs.Checked)
            {
                foreach (CheckBox c in eflayer.Controls.OfType<CheckBox>())
                    c.Checked = true;
            }
            else
            {
                foreach (CheckBox c in eflayer.Controls.OfType<CheckBox>())
                    c.Checked = false;
            }
            
        }

        public async Task<int> backupFILES()
        {
            try
            {
                if (!Directory.Exists(GetGames + @"\Sensei_Backup"))
                    Directory.CreateDirectory(GetGames + @"\Sensei_Backup");

                var getf = pf._allEFFECTS;
                for (int i = 0; i < getf.Count; i++)
                {
                    
                    if (new FileInfo(GetDEPath + @"\resources\_common\particles\textures\atlases\" + getf[i]).Length != 0)
                       await CopyFileAsync(GetDEPath + @"\resources\_common\particles\textures\atlases\" + getf[i], GetGames + @"\Sensei_Backup\" + getf[i]);
                }
                if(progCOPY.Value > 0)
                {
                    progCOPY.Value = progCOPY.Maximum;
                    progCOPY.Visible = false;
                }
                else
                    progCOPY.Visible = false;

                return 0;
            }
            catch(SystemException)
            {
                MessageBoxEx.Show("Restart Sensei DE as Administrator.", "Not Enough Permissions!");
                return 0;
            }
            
                
        }
        private async void efoff_Click(object sender, System.EventArgs e)
        {
            progCOPY.Visible = true;
            progCOPY.Value = 0;
            int selec = 0;
            foreach (CheckBox c in eflayer.Controls.OfType<CheckBox>())
                if (c.Checked)
                    selec++;
            if(selec == 0)
            {
                MessageBoxEx.Show("Please Check at least one or more effects first.", "Alert!");
                return;
            }
                

            try
            {
                var getf = pf._allEFFECTS;
                //Set progress counter
                progCOPY.Maximum = selec;
                foreach (CheckBox c in eflayer.Controls.OfType<CheckBox>())
                {
                    if (c.Checked)
                        await EmptyFileAsync(GetDEPath + @"\resources\_common\particles\textures\atlases\" + getf[int.Parse(c.Tag.ToString())]);
                }

            }
            catch(SystemException)
            {

            }
            progCOPY.Visible = false;
            MessageBoxEx.Show("Success! " + selec.ToString() + " Effect(s) & Animation(s) Disabled!");
                    

        }

        private async void restef_Click(object sender, EventArgs e)
        {
            progCOPY.Visible = true;
            progCOPY.Value = 0;
            progCOPY.Maximum = Directory.GetFiles(GetGames + @"\Sensei_Backup", "*.json").Count();
            foreach (string file in Directory.GetFiles(GetGames + @"\Sensei_Backup","*"))
                await CopyFileAsync(file, GetDEPath + @"\resources\_common\particles\textures\atlases\" + Path.GetFileName(file));

            progCOPY.Visible = false;
            foreach (CheckBox c in eflayer.Controls.OfType<CheckBox>())
                c.Checked = false;

            MessageBoxEx.Show("Success! " + "All Game Effects have been Restored.");
        }
    }

    
}
