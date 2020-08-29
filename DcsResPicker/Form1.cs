using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Welcome to DReLa (DCS Resolution Launcher). This utility is created to enable users
 * to choose the game resolution right before the game launches. Created for mission makers,
 * those who want to play on a different screen, or those who simply want to change
 * their DCS resolution without launching DCS twice.
 */

//sources
//https://docs.microsoft.com/en-us/visualstudio/ide/how-to-specify-an-application-icon-visual-basic-csharp?view=vs-2019
//https://www.geeksforgeeks.org/c-sharp-string-contains-method/
//https://stackoverflow.com/questions/15582441/how-to-get-index-of-word-inside-a-string
//https://www.geeksforgeeks.org/c-sharp-string-indexof-method-set-1/#:~:text=In%20C%23%2C%20IndexOf()%20method,or%20string%20is%20not%20found.
//https://docs.microsoft.com/en-us/dotnet/api/system.string.remove?view=netcore-3.1
//https://stackoverflow.com/questions/9679375/run-an-exe-from-c-sharp-code/40019420
//https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-a-text-file-one-line-at-a-time

namespace DcsResPicker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //things that are done on program run

            //If there is already a settings file, load it
            if (File.Exists(appPath + @"/DCS-Resolution-Launcher-Settings/DResLa-UserSettings.txt"))
            {
                //MessageBox.Show("Loading...");
                System.IO.StreamReader file = new System.IO.StreamReader(appPath + @"/DCS-Resolution-Launcher-Settings/DResLa-UserSettings.txt");
                //read the file line by line. Assumes the lines are properly formated via the saveSettings method
                //if no custom resoution was saved, the lines will be blank, which is ok because the fields will also be blank as a result
                fullPath_dcsExe = file.ReadLine();
                fullPath_optionsLua = file.ReadLine();
                customWidth = file.ReadLine();
                customHeight = file.ReadLine();
                file.Close();
                //set the textboxes to the data that was read
                textBox1_dcsPath.Text = fullPath_dcsExe;
                textBox2_optionsPath.Text = fullPath_optionsLua;
                textBox3_customWidth.Text = customWidth;
                textBox4_customHeight.Text = customHeight;
            }

        }

        string appPath = Application.StartupPath;//gets the path of were the utility us running
        int selectedWidth;
        int selectedHeight;
        string customWidth;//string for the save file
        string customHeight;//string for the save file
        string optionsLuaContents;
        //makes sure that the user actually picked the correct files instead of a random exe or .lua
        string correctDcsExeCheck = "DCS.exe";
        string correctOptionsLuaCheck = "options.lua";

        string fullPath_dcsExe = string.Empty;

        private void button1_selectDCS_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\Program Files\\Eagle Dynamics\\DCS World\\bin\\";//likely not necessary, but it may help
                openFileDialog.Filter = "exe files (*.exe)|*.exe";//pick an exe only
                //openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Select DCS.exe (Hint: C:\\Program Files\\Eagle Dynamics\\DCS World\\bin\\DCS.exe";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file with the extention
                    fullPath_dcsExe = openFileDialog.FileName;
                    if (fullPath_dcsExe.Contains(correctDcsExeCheck))
                    {
                        textBox1_dcsPath.Text = fullPath_dcsExe;
                    }
                    else {
                        MessageBox.Show("It looks like you did not select the correct file. Please try again.");
                        return; }
                    
                }

            }
        }

        string fullPath_optionsLua = string.Empty;
        private void button2_selectOptions_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\Users";//gets closer to the location of the file
                openFileDialog.Filter = "lua files (*.lua)|*.lua";//select lua files only
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Select options.lua (Hint: C:\\Users\\YOURNAME\\Saved Games\\DCS\\Config\\options.lua";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file with the extenstion
                    fullPath_optionsLua = openFileDialog.FileName;
                    if (fullPath_optionsLua.Contains(correctOptionsLuaCheck))
                    {
                        textBox2_optionsPath.Text = fullPath_optionsLua;
                    }
                    else {
                        MessageBox.Show("It looks like you did not select the correct file. Please try again.");
                        return; }
                    
                }

            }
        }

        int indexOfWidth;
        int indexOfHeight;
        int indexOfCommaAfterWidth;
        int indexOfCommaAfterHeight;
        string optionsLuaContents_region1; //the part from the start to height
        string optionsLuaContents_region2; //the part from the height to width
        string optionsLuaContents_region3; //the part from the width to end
        string newoptionsLuaContents;
        private void replaceWidthAndHeight()
        {
            optionsLuaContents = File.ReadAllText(fullPath_optionsLua);//reads the users options.lua
            indexOfWidth = optionsLuaContents.IndexOf("width");
            indexOfCommaAfterWidth = optionsLuaContents.IndexOf(",", indexOfWidth + 1);
            indexOfHeight = optionsLuaContents.IndexOf("height");
            indexOfCommaAfterHeight = optionsLuaContents.IndexOf(",", indexOfHeight + 1);

            optionsLuaContents_region1 = optionsLuaContents.Substring(0, indexOfHeight);
            optionsLuaContents_region2 = optionsLuaContents.Substring(indexOfCommaAfterHeight, indexOfWidth- indexOfCommaAfterHeight);
            optionsLuaContents_region3 = optionsLuaContents.Substring(indexOfCommaAfterWidth);

            //merge and reconstruct the options.lua while inserting the new height and width. 
            newoptionsLuaContents = (optionsLuaContents_region1 + "height\"] = " + selectedHeight.ToString() + optionsLuaContents_region2 + "width\"] = " + selectedWidth.ToString() + optionsLuaContents_region3);
            
            File.WriteAllText(fullPath_optionsLua, newoptionsLuaContents);//this is the command that actually creates the lua

            saveUserSettings();//after the file is written, save the settings

            var proc = Process.Start(fullPath_dcsExe, "--force_disable_VR");//start the game in flatscreen
            //https://stackoverflow.com/questions/7008647/running-exe-with-parameters/7008741

            exitProgram();//exit the program, maybe
        }

        private void saveUserSettings()
        {
            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
            string[] exportLines = { fullPath_dcsExe, fullPath_optionsLua, customWidth, customHeight };
            Directory.CreateDirectory(appPath + "\\DCS-Resolution-Launcher-Settings");
            File.WriteAllLines(appPath + "\\DCS-Resolution-Launcher-Settings\\DResLa-UserSettings.txt", exportLines);
        }

        private void exitProgram()
        {
            //if the exit box is checked, exit the program
            if (checkBox1.Checked == true)
            {
                Application.Exit();
                //https://stackoverflow.com/questions/12977924/how-to-properly-exit-a-c-sharp-application
            }
            else { return; }
           
        }


        private void button3_launchDCS_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(fullPath_dcsExe) || String.IsNullOrEmpty(fullPath_optionsLua))//check if the values are set
            {
                MessageBox.Show("It looks like you did not select the correct DCS Path and options.lua. Please try again.");
                return;
            }
            if (String.IsNullOrEmpty(textBox3_customWidth.Text.ToString()) || String.IsNullOrEmpty(textBox4_customHeight.Text.ToString()))
            {
                MessageBox.Show("Please enter acceptable resolution width and height.");
                return;
            }

            else
            {
                customWidth = textBox3_customWidth.Text;//save for the save file
                customHeight = textBox4_customHeight.Text;//save for the save file

                selectedWidth = Convert.ToInt32(textBox3_customWidth.Text);
                selectedHeight = Convert.ToInt32(textBox4_customHeight.Text);

                replaceWidthAndHeight();
            }
        }

        private void button10_1920x1080_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(fullPath_dcsExe) || String.IsNullOrEmpty(fullPath_optionsLua))
                //if one of the two paths is empty or null, the user didnt have them set
            {
                MessageBox.Show("It looks like you did not select the correct DCS Path and options.lua. Please try again.");
                return;
            }
            else
            {
                selectedWidth = 1920;
                selectedHeight = 1080;
                replaceWidthAndHeight();
            }
            
        }

        private void button11_2560x1080_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(fullPath_dcsExe) || String.IsNullOrEmpty(fullPath_optionsLua))
            //if one of the two paths is empty or null, the user didnt have them set
            {
                MessageBox.Show("It looks like you did not select the correct DCS Path and options.lua. Please try again.");
                return;
            }
            else
            {
                selectedWidth = 2560;
                selectedHeight = 1080;
                replaceWidthAndHeight();
            }
        }

        

        private void textBox3_customWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            //numbers only
            //https://ourcodeworld.com/articles/read/507/how-to-allow-only-numbers-inside-a-textbox-in-winforms-c-sharp
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox4_customHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            //numbers only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void button4_resetOptionsLua_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello and welcome to DCS Resolution Launcher (DReLa) v1." 
                + " This program creates and modifies files on your computer. If you are not ok with that, please do not use this utility."
                + "\r\n" + "\r\n"
                + "1. Select your DCS.exe file. It is usually located in C:\\Program Files\\Eagle Dynamics\\DCS World\\bin."
                + "\r\n"
                + "2. Select your options.lua file. It is usually located in C:\\Users\\YOURNAME\\Saved Games\\DCS\\Config."
                + "\r\n"
                + "3. Select a preset resolution. DCS will automatically launch. Or..."
                + "\r\n"
                + "4. Enter a custom resolution and click Launch with Custom Res."
                + "\r\n"
                + "5. Right before DCS launches, DReLa will edit your options.lua and save your settings in a text file in the location of this application." +
                " The next time you run the program you won't have to set the DCS and options.lua paths."
                + "\r\n" + "\r\n"
                + "Thank you to Captian Bird of the Hoggit Discord for the tips and idea for this utility. If you have any comments, concerns, wishes," +
                " or just want to say Hi, contact me via Discord: Bailey#6230." +
                "\r\n" +
                "Please feel free to donate. All donations go to making more free DCS utilities and mods for the community! " +
                " https://www.paypal.com/paypalme/asherao" + "\r\n" + "\r\n"
                + "Enjoy!" + "\r\n"+ "\r\n"+

                "If you would like to examine, follow, or add to DReLa, the git is here: https://github.com/asherao/DCS-Resolution-Launcher--DReLa-" +

                "\r\n" + "\r\n" + "~Bailey" + "\r\n" + "29AUG2020"
                );
        }

        private void button12_3840x2160_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(fullPath_dcsExe) || String.IsNullOrEmpty(fullPath_optionsLua))
            //if one of the two paths is empty or null, the user didnt have them set
            {
                MessageBox.Show("It looks like you did not select the correct DCS Path and options.lua. Please try again.");
                return;
            }
            else
            {
                selectedWidth = 3840;
                selectedHeight = 2160;
                replaceWidthAndHeight();
            }
        }


        private void label1_dcsPath_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button13_1280x720_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(fullPath_dcsExe) || String.IsNullOrEmpty(fullPath_optionsLua))
            //if one of the two paths is empty or null, the user didnt have them set
            {
                MessageBox.Show("It looks like you did not select the correct DCS Path and options.lua. Please try again.");
                return;
            }
            else
            {
                selectedWidth = 1280;
                selectedHeight = 720;
                replaceWidthAndHeight();
            }
        }
    }
}
