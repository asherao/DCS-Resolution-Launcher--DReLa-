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
 * their DCS resolution without launching DCS twice. It modifies the 'width' and 'height' values
 * of the users option.lua. You can also enter and save custom resolutions.
 * 
 * Good program window size seems to be about (243, 361)
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
        string resolutionToLoad;
        int numberOfLinesInSaveFile;
        public Form1()
        {
            InitializeComponent();
            //things that are done on program run, i think

            //If there is already a settings file, load it
            if (File.Exists(appPath + @"/DCS-Resolution-Launcher-Settings/DReLa-UserSettings.txt"))
            {
                //MessageBox.Show("Loading...");
                System.IO.StreamReader file = new System.IO.StreamReader(appPath + @"/DCS-Resolution-Launcher-Settings/DReLa-UserSettings.txt");
                //read the file line by line. Assumes the lines are properly formated via the saveSettings method
                //e.g. first line is the users dcs.exe location. The second line is the users options.lua location
             
                fullPath_dcsExe = file.ReadLine();//read the first line
                fullPath_optionsLua = file.ReadLine();//read the second line
                textBox1_dcsPath.Text = fullPath_dcsExe;//put the first line in the first box
                textBox2_optionsPath.Text = fullPath_optionsLua;//put the second line in the seecond box

                while ((resolutionToLoad = file.ReadLine()) != null)//while the variable actually grabbed something from the file
                    {
                    if (resolutionToLoad.Equals(""))//if it grabbed an 'enter' character (should only happen if the user did odd things)
                    {
                        //this exclusvely protects against there being blank lines after line 2 of the save file
                        comboBox1_widthHeightRes.Items.Add("1280 x 720");
                        comboBox1_widthHeightRes.Items.Add("1920 x 1080");
                        comboBox1_widthHeightRes.Items.Add("2560 x 1080");
                        comboBox1_widthHeightRes.Items.Add("2560 x 1440");
                        comboBox1_widthHeightRes.Items.Add("3840 x 2160");
                        return;
                    }
                    else
                    {
                        //adds the saved resolutions to the combobox
                        comboBox1_widthHeightRes.Items.Add(resolutionToLoad);
                    }
                }

                file.Close();//closes the read stream
                
                //put the number of lines that were in the save file in a variable
                numberOfLinesInSaveFile = File.ReadAllLines(appPath + @"/DCS-Resolution-Launcher-Settings/DReLa-UserSettings.txt").Length;

                if (numberOfLinesInSaveFile  < 3)//less than 3 lines means that there should be no resolutions saved
                {
                    comboBox1_widthHeightRes.Items.Add("1280 x 720");
                    comboBox1_widthHeightRes.Items.Add("1920 x 1080");
                    comboBox1_widthHeightRes.Items.Add("2560 x 1080");
                    comboBox1_widthHeightRes.Items.Add("2560 x 1440");
                    comboBox1_widthHeightRes.Items.Add("3840 x 2160");
                }
            }

           else//this else is for the event that there was no file to load
            {
                //comboBox1_widthHeightRes.Items.Add("");
                comboBox1_widthHeightRes.Items.Add("1280 x 720");
                comboBox1_widthHeightRes.Items.Add("1920 x 1080");
                comboBox1_widthHeightRes.Items.Add("2560 x 1080");
                comboBox1_widthHeightRes.Items.Add("2560 x 1440");
                comboBox1_widthHeightRes.Items.Add("3840 x 2160");
            }
        }

        string appPath = Application.StartupPath;//gets the path of were the utility us running
        int selectedWidth;
        int selectedHeight;
        string optionsLuaContents;

        //makes sure that the user actually picked the correct files instead of a random exe or .lua
        string correctDcsExeCheck = "DCS.exe";
        string correctOptionsLuaCheck = "options.lua";

        string fullPath_dcsExe = string.Empty;

        private void button1_selectDCS_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())//user pics dcs.exe via file dialog
            {
                openFileDialog.InitialDirectory = "c:\\Program Files\\Eagle Dynamics\\DCS World\\bin\\";//likely not necessary, but it may help
                openFileDialog.Filter = "exe files (*.exe)|*.exe";//pick an exe only
                //openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Select DCS.exe (Hint: C:\\Program Files\\Eagle Dynamics\\DCS World\\bin\\DCS.exe)";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file with the extention
                    fullPath_dcsExe = openFileDialog.FileName;
                    if (fullPath_dcsExe.Contains(correctDcsExeCheck))
                    {
                        textBox1_dcsPath.Text = fullPath_dcsExe;//put the chosen file in the box
                    }
                    else {
                        MessageBox.Show("It looks like you did not select the correct file. Please try again.");
                        return; 
                    }
                }
            }
        }

        string fullPath_optionsLua = string.Empty;
        private void button2_selectOptions_Click(object sender, EventArgs e)//same as dcs.exe., but for options.lua file
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
                        textBox2_optionsPath.Text = fullPath_optionsLua;//put the chosen file in the box
                    }
                    else {
                        MessageBox.Show("It looks like you did not select the correct file. Please try again.");
                        return; 
                    }
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
        private void replaceWidthAndHeight()//if the user has a differently configured options.lua, this breaks
            //eg, if 'height' comes before 'width' or something.
        {
            optionsLuaContents = File.ReadAllText(fullPath_optionsLua);//reads the users options.lua
            //find the index of 'height' and 'width' for future calculations
            indexOfWidth = optionsLuaContents.IndexOf("width");
            indexOfCommaAfterWidth = optionsLuaContents.IndexOf(",", indexOfWidth + 1);
            indexOfHeight = optionsLuaContents.IndexOf("height");
            indexOfCommaAfterHeight = optionsLuaContents.IndexOf(",", indexOfHeight + 1);

            //splits the options.lua into three chunks
            optionsLuaContents_region1 = optionsLuaContents.Substring(0, indexOfHeight);
            optionsLuaContents_region2 = optionsLuaContents.Substring(indexOfCommaAfterHeight, indexOfWidth- indexOfCommaAfterHeight);
            optionsLuaContents_region3 = optionsLuaContents.Substring(indexOfCommaAfterWidth);

            //merge and reconstruct the options.lua while inserting the new height and width. 
            newoptionsLuaContents = (optionsLuaContents_region1 + "height\"] = " + selectedHeight.ToString() + optionsLuaContents_region2 + "width\"] = " + selectedWidth.ToString() + optionsLuaContents_region3);
            
            File.WriteAllText(fullPath_optionsLua, newoptionsLuaContents);//this is the command that actually creates the lua

            
        }

        string uniqueRes;
        private void saveUserSettings()
        {
            //export the 2 directories that the user choose to a .txt file
            //export all of the preset resolutions

            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
            //string[] exportLines = { fullPath_dcsExe, fullPath_optionsLua, customWidth, customHeight };
            Directory.CreateDirectory(appPath + "\\DCS-Resolution-Launcher-Settings");//creates the save folder
            //File.WriteAllLines(appPath + "\\DCS-Resolution-Launcher-Settings\\DReLa-UserSettings.txt", exportLines);
            ////https://docs.microsoft.com/en-us/dotnet/api/system.io.streamwriter?redirectedfrom=MSDN&view=netcore-3.1
            using (StreamWriter sw = File.CreateText(appPath + "\\DCS-Resolution-Launcher-Settings\\DReLa-UserSettings.txt"))
            {
                sw.WriteLine(fullPath_dcsExe);
                sw.WriteLine(fullPath_optionsLua);

                foreach(object listOfSingleRes in comboBox1_widthHeightRes.Items)//for each of the item in the combobox
                    //do this loop
                {
                    uniqueRes = listOfSingleRes.ToString();//put the resolution in a variable
                    sw.WriteLine(uniqueRes);//write the contents of the variable to the .txt
                    //because of the 'for each' this will then move to the next object in the list and continue
                }
            }
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
            /*old buttons from DReLa v1
            if (String.IsNullOrEmpty(fullPath_dcsExe) || String.IsNullOrEmpty(fullPath_optionsLua))//check if the values are set
            {
                MessageBox.Show("It looks like you did not select the correct DCS Path and options.lua. Please try again.");
                return;
            }
            else
            {
                replaceWidthAndHeight();
                saveUserSettings();//after the file is written, save the settings

                //this launches dcs
                //https://stackoverflow.com/questions/7008647/running-exe-with-parameters/7008741
                var proc = Process.Start(fullPath_dcsExe, "--force_disable_VR");//start the game in flatscreen

                exitProgram();//exit the program if the user left the checkbox checked
            }
            */
        }

        private void button10_1920x1080_Click(object sender, EventArgs e)
        {
            /*old buttons from DReLa v1
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
            */
        }

        private void button11_2560x1080_Click(object sender, EventArgs e)
        {
            /*old buttons from DReLa v1
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
            */
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

        private void button5_help_Click(object sender, EventArgs e)//text for the relp / readmee
        {
            MessageBox.Show("Hello and welcome to DCS Resolution Launcher (DReLa) v2." 
                + " This program creates and modifies files on your computer. If you are not ok with that, please do not use this utility."
                + "\r\n" + "\r\n"
                + "1. Select your DCS.exe file. It is likely located in C:\\Program Files\\Eagle Dynamics\\DCS World\\bin."
                + "\r\n"
                + "2. Select your options.lua file. It is likely located in C:\\Users\\YOURNAME\\Saved Games\\DCS\\Config."
                + "\r\n"
                + "3. Pick resolution. Or..."
                + "\r\n"
                + "4. Enter a custom resolution and click 'Add Custom Resolution'."
                + "\r\n"
                + "5. Click 'Launch DCS'. Right before DCS launches, DReLa will edit your 'options.lua' and save your settings in a text file in the location of this application." +
                " The next time you run the program you won't have to set the DCS and 'options.lua' paths and your custom resolutions will be there for you."
                + "\r\n" + "\r\n"
                + "Thank you to Captain Bird of the Hoggit Discord for the tips and idea for this utility. Thank you to those of the" +
                " ED forums for the feedback. If you have any comments, concerns, wishes," +
                " or just want to say Hi, contact me via Discord: Bailey#6230." +
                "\r\n" +
                "Please feel free to donate. All donations go to making more free DCS utilities and mods for the community! " +
                " https://www.paypal.com/paypalme/asherao" + "\r\n" + "\r\n"
                + "Enjoy!" + "\r\n"+ "\r\n"+

                "If you would like to examine, follow, or add to DReLa, the git is here: https://github.com/asherao/DCS-Resolution-Launcher--DReLa-" +

                "\r\n" + "\r\n" + "~Bailey" + "\r\n" + "29AUG2020");
        }

        private void button12_3840x2160_Click(object sender, EventArgs e)
        {
            /*old buttons from DReLa v1
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
            */
        }

        private void label1_dcsPath_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button13_1280x720_Click(object sender, EventArgs e)
        {
            /*old buttons from DReLa v1
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
            */
        }

        private void textBox5_customWidthEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            //numbers only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox6_customHeightEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            //numbers only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        string userCustomResWidth;
        string userCustomResHeight;

        int numberOfCustomResolutions;
        private void button6_addCustomResToList_Click(object sender, EventArgs e)//adds the user enter custom res to the combobox
        {
            if (textBox5_customWidthEntry.TextLength > 1 && textBox6_customHeightEntry.TextLength > 1)//if the user entry is more than 1 digit
            {
                userCustomResWidth = textBox5_customWidthEntry.Text;
                userCustomResHeight = textBox6_customHeightEntry.Text;
                comboBox1_widthHeightRes.Items.Add(userCustomResWidth + " x " + userCustomResHeight);//adds the user res to the combobox

                numberOfCustomResolutions = Convert.ToInt32(comboBox1_widthHeightRes.Items.Count.ToString());//counts the number of res in the combobox
                comboBox1_widthHeightRes.SelectedIndex = numberOfCustomResolutions - 1;//selects the last res, which should be
                //the one that the user just entered. Note the '- 1'. That is because the index itself is 0 based. 

                textBox5_customWidthEntry.Text = "";//sets the windows to blank to let the user know it worked
                textBox6_customHeightEntry.Text = "";//sets the windows to blank to let the user know it worked
            }
            else
            {
                MessageBox.Show("Please enter a higher resolution.");//this happens if the user enters a 1 digit resolution
            }
        }

        private void button7_addCustomResToList_Click(object sender, EventArgs e)//removes the selected res from the combobox
        {
            if (comboBox1_widthHeightRes.Items.Count == 1)
            {
                //this is here just so that the combo box always has something it it, preventing unforseen errors, maybe
                MessageBox.Show("You can't remove all resolutions. Please add one before removing another.");
                return;
            }
            else
            {
                comboBox1_widthHeightRes.Items.Remove(comboBox1_widthHeightRes.SelectedItem);
            }
        }

        string chosenRes;
        int indexOfX;

        private void button8_launchDCS_Click(object sender, EventArgs e)//launches dcs after a few checks
        {
            if (String.IsNullOrEmpty(fullPath_dcsExe) || String.IsNullOrEmpty(fullPath_optionsLua))//check if the values are set
            {
                MessageBox.Show("It looks like you did not select the correct DCS Path and options.lua. Please try again.");
                return;
            }
            if (comboBox1_widthHeightRes.Text.Length == 0)//if the combobox is empty, eg, a ras has not been selected
            {
                MessageBox.Show("Please select an acceptable resolution width and height.");
                return;
            }

            else
            {
                //this section un-parses the selected resoluttion. It is read directly from the combobox
                chosenRes = comboBox1_widthHeightRes.SelectedItem.ToString();

                indexOfX = chosenRes.IndexOf("x");//the x character is what splits the numbers
                selectedWidth = Convert.ToInt32(chosenRes.Substring(0, indexOfX - 1));//typical index math to select what i want
                selectedHeight = Convert.ToInt32(chosenRes.Substring(indexOfX + 1));//typical index math to select what i want

                //MessageBox.Show("|" + selectedWidth + "|" + selectedHeight + "|");

                replaceWidthAndHeight();//modifies the options.lua
                saveUserSettings();//after the file is written, save the settings

                //this launches dcs
                //https://stackoverflow.com/questions/7008647/running-exe-with-parameters/7008741
                var proc = Process.Start(fullPath_dcsExe, "--force_disable_VR");//start the game in flatscreen

                exitProgram();//exit the program if the user left the checkbox checked
            }
        }

        private void textBox6_customHeightEntry_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
