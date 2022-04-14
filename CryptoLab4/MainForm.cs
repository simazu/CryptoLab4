using CryptoLab4.Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace CryptoLab4
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void getHashButton_Click(object sender, EventArgs e)
        {
            string message = ioRichTextBox.Text;
            MD4 md4 = new MD4();
            ioRichTextBox.Text += "\n\nMD4: " + md4.GetHexHashFromString(message);
        }

        private void plotButton_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            switch (chartComboBox.SelectedIndex)
            {
                case 0:
                    {
                        (int[], int[]) oneRoundAvalancheEffectResult = MD4Tester.AvalancheEffectTest(false, false);
                        (int[], int[]) twoRoundAvalancheEffectResult = MD4Tester.AvalancheEffectTest(true, false);
                        (int[], int[]) threeRoundAvalancheEffectResult = MD4Tester.AvalancheEffectTest();
                        // TODO
                        break;
                    }
                case 1:
                    {
                        (int[], int[]) defaultConstantsAvalancheEffectResult = MD4Tester.AvalancheEffectTest();
                        (int[], int[]) randomConstantsAvalancheEffectResult = MD4Tester.AvalancheEffectTest(true, true,
                            0xaaaaaaaa, 0xaaaaaaaa, 0xaaaaaaaa, 0xaaaaaaaa);
                        //TODO
                        break;
                    }
                case 2:
                    {
                        int messageLengthInBits = 80;
                        (int[] k, (string, string)[] collisions, string[] collisionHashes, int[] N)
                            = MD4Tester.CollisionsTest(messageLengthInBits);
                        //TODO N(k)
                        break;
                    }
                case 3:
                    {
                        string prototype = "random text";
                        (int[] k, string[] prototypes, int[] N) = MD4Tester.PrototypesTest(prototype);
                        //TODO N(k)
                        break;
                    }
                case 4:
                    {
                        (int[] lengths, TimeSpan[] hashingTimes) = MD4Tester.HashingTimeFromMessageLength();
                        //TODO
                        break;
                    }

            }
        }

        private void findCollisionButton_Click(object sender, EventArgs e)
        {
            ((string, string) collision, string hash, int numGeneratedStrings) 
                = MD4Tester.FindCollision((int)messageLengthNumericUpDown.Value, (int)hashLengthNumericUpDown.Value);
            ioRichTextBox.Clear();
            ioRichTextBox.Text += $"------------------\ncollision\n------------------\n{collision.Item1}\n\n{collision.Item2}\n\n" +
                $"------------------\nhash\n------------------\n{hash}\n\ngenerated strings: {numGeneratedStrings}";
        }

        private void findPrototypeButton_Click(object sender, EventArgs e)
        {
            (string prototype, string hash, int numGeneratedStrings)
                = MD4Tester.FindPrototype(ioRichTextBox.Text, (int)hashLengthNumericUpDown.Value);
            ioRichTextBox.Text += $"\n\n------------------\nprototype\n------------------\n{prototype}\n\n" +
                $"------------------\nhash\n------------------\n{hash}\n\ngenerated strings: {numGeneratedStrings}";
        }
    }
}
