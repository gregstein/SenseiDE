using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE_Sensei
{
    class perfCLASS
    {
        public List<string> _Effects = new List<string>
        {
           "fire.json",
           "blood.json",
           "dock_seagulls.json",
           "muzzle_cannongalleon.json",
           "research_glow.json",
           "upgrade_buildings.json",
           "water_splashes2x2.json",
           "water_splashes3x3.json",
           "impact_explosions.json",
           "waterfall.json",
           "explosion_demo_ships.json",
           "impact_dust.json",
           "impact_trebuchet_fire_atlas.json",
           "impact_petard.json",
           "leaves_falling.json",
           "muzzle_bombardcannon.json",
           "muzzle_organgun.json",
           "snow_footsteps.json",
           "snow_tracks_trebuchet_32_directions.json",
           "tarkan_torch.json",
           "wake_back.json",
           "wake_front.json",
           "water_destruction.json",
           "smoke_chimney.json",
           "gold_shimmer.json",
           "heroglow_small.json",
           "impact_laser_explosion.json",
           "impact_water.json",
           "impact_water_steam.json",
           "muzzle_conquistador.json",
           "muzzle_handcannon.json",
           "relic_glow.json",
           "villager_forage.json",
           "villager_gold_mining.json",
           "villager_stone_mining.json",
           "water_footprint.json",
           "heroglow_big.json",
           "heroglow_medium.json",

        };
        public void RegEdit(string Keyname, object Value, RegistryValueKind Regtype)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Microsoft Games\Age of Empires II DE", true)) //must dispose key or use "using" keyword
            {
                if (key != null) 
                {
                    key.SetValue(Keyname, Value, Regtype);
                }
            }
        }
        public bool senseiBORN()
        {
            using (RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\SenseiDE"))
            {
                if((Registry.GetValue(key.Name, "Splash Screen Pixelate", "")) != null)
                    key.SetValue("Splash Screen Pixelate", 0, Microsoft.Win32.RegistryValueKind.DWord);

                if ((Registry.GetValue(key.Name, "Skip Intro", "")) != null)
                    key.SetValue("Skip Intro", 0, Microsoft.Win32.RegistryValueKind.DWord);

                if ((Registry.GetValue(key.Name, "Disable Effects", "")) != null)
                    key.SetValue("Disable Effects", 0, Microsoft.Win32.RegistryValueKind.DWord);

                if ((Registry.GetValue(key.Name, "Mods Manager", "")) != null)
                    key.SetValue("Mods Manager", 0, Microsoft.Win32.RegistryValueKind.DWord);

                if ((Registry.GetValue(key.Name, "Performance", "")) != null)
                    key.SetValue("Performance", 0, Microsoft.Win32.RegistryValueKind.DWord);

                if (key.GetValue("Shrink") != null)
                    key.SetValue("Shrink", 100, Microsoft.Win32.RegistryValueKind.DWord);
                return true;
            }
            
        }
        public void EXEsettings(string Keyname, object Value, RegistryValueKind Regtype)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", true)) //must dispose key or use "using" keyword
            {
                if (key != null)
                {
                    key.SetValue(Keyname, Value, Regtype);
                }
            }
        }
        public bool KidsHERE()
        {
            RegistryKey kidnames = Registry.CurrentUser.OpenSubKey(@"Software\SenseiDE", true);
            return (kidnames.GetValueNames().Count() == 6);
        }
        public bool senseiEXISTS()
        {
            RegistryKey SensKey = Registry.CurrentUser.OpenSubKey(@"Software\SenseiDE", true);
            return (SensKey != null);
        }
        public void SenseiREG(string Keyname, object Value, RegistryValueKind Regtype)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\SenseiDE", true)) //must dispose key or use "using" keyword
                key.SetValue(Keyname, Value, Regtype); 
        }
        public void RetSETTINGS(string RegKeyvalue, DevComponents.DotNetBar.Controls.SwitchButton SB)
        {
            if (HearSensei(RegKeyvalue, Microsoft.Win32.RegistryValueKind.DWord))
                SB.ValueObject = "Y";
            else
                SB.ValueObject = "N";
        }
        public void RetSETTINGS2(string RegKeyvalue, ComponentFactory.Krypton.Toolkit.KryptonTrackBar SB, System.Windows.Forms.Label lb)
        {

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\SenseiDE", true))
                {
                if (key.GetValue("Shrink") == null)
                    key.SetValue("Shrink", 100, Microsoft.Win32.RegistryValueKind.DWord);

                if (key != null)
                    {
                        string valE = key.GetValue(RegKeyvalue).ToString();
                        if (valE != null)
                        {
                            lb.Text = valE + "%";
                            SB.Value = int.Parse(valE);
                         }
                            
                    }
                }
            
        }
        public void RetSETTINGS3(string RegKeyvalue, DevComponents.DotNetBar.Controls.ComboBoxEx CB)
        {

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\SenseiDE", true))
            {
                if (key.GetValue("Performance") == null)
                {
                    
                    key.SetValue("Performance", 0, Microsoft.Win32.RegistryValueKind.DWord);
                    CB.Text = "Default";
                }
                    

                if (key != null)
                {
                    string valE = key.GetValue(RegKeyvalue).ToString();
                    if (valE == "1")
                    {
                        CB.Text = "High Performance";
                    }
                    else if(valE == "0")
                    {
                        CB.Text = "Default";
                    }

                }
            }

        }
        public void CheckLayers(string gamepath)
        {
            RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry64); //here you specify where exactly you want your entry

            var reg = localMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", true);
            if (reg == null)
            {
                reg = localMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers");
            }

            if (reg.GetValue(gamepath) == null)
            {
                reg.SetValue(gamepath, "");
            }
        }
        public void RetSETTINGS4(string RegKeyvalue, DevComponents.DotNetBar.Controls.CheckBoxX CB, string CBvalue)
        {

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", true))
            {


                if (key != null)
                {
                    string valE = key.GetValue(RegKeyvalue).ToString()??"";
                    
                    if (valE.Contains(CBvalue))
                    {
                        CB.Checked = true;
                    }
                    else
                    {
                        CB.Checked = false;
                    }

                }
            }

        }
        public string EXEread(string EXEpath)
        {
         
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", true))
            {
              

                if (key != null)
                {
                    string valE = key.GetValue(EXEpath).ToString()??"";
                    if (valE != null)
                    {
                        return valE;
                    }
                    else
                    {
                        return "";
                    }

                }
            }
            return "";
        }
        public async Task<int> importRegistry(string filepath)
        {
            try
            {
                Process regeditProcess = Process.Start("regedit.exe", "/s " + "\"" + filepath + "\"");
                regeditProcess.WaitForExit();
                regeditProcess.Close();
                return 1;
            }
            catch (SystemException)
            {
                // handle exception
                return 0;
            }
        }
        public bool HearSensei(string Keyname, RegistryValueKind Regtype)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\SenseiDE", true)) 
            {
                if (key != null)
                {
                    string valE = (Registry.GetValue(key.Name, Keyname, "")).ToString();
                    if (valE == "1")
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public async Task CopyFileAsync(string sourcePath, string destinationPath, DevComponents.DotNetBar.Controls.ProgressBarX bar)
        {
            using (Stream source = File.OpenRead(sourcePath))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                    bar.Value++;
                }
            }

        }

    }
}
