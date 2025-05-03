using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace MiniPiano
{
    public partial class Form1 : Form
    {
        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool Beep(int frequency, int duration);

        private Dictionary<Keys, int> keyToFrequency = new Dictionary<Keys, int>();
        private Dictionary<Button, int> buttonToFrequency = new Dictionary<Button, int>();
        private HashSet<Keys> pressedKeys = new HashSet<Keys>(); // Отслеживание активных клавиш
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            //привязка кнопок к частотам 
            buttonToFrequency.Add(button1, 698);//F2
            buttonToFrequency.Add(button2, 659);//E2
            buttonToFrequency.Add(button3, 523);//D2
            buttonToFrequency.Add(button4, 587);//C2
            buttonToFrequency.Add(button5, 493);//H
            buttonToFrequency.Add(button6, 440);//A
            buttonToFrequency.Add(button7, 392);//G
            buttonToFrequency.Add(button8, 349);//F
            buttonToFrequency.Add(button9, 329);//E
            buttonToFrequency.Add(button10, 293);//D
            buttonToFrequency.Add(button11, 220);//C
            buttonToFrequency.Add(button12, 246);//h
            buttonToFrequency.Add(button13, 261);//a
            buttonToFrequency.Add(button14, 196);//g
            buttonToFrequency.Add(button15, 174);//f
            buttonToFrequency.Add(button16, 185);//fis
            buttonToFrequency.Add(button17, 207);//gis
            buttonToFrequency.Add(button18, 233);//ais
            buttonToFrequency.Add(button19, 277);//CIS
            buttonToFrequency.Add(button20, 311);//DIS
            buttonToFrequency.Add(button21, 369);//FIS
            buttonToFrequency.Add(button22, 415);//GIS
            buttonToFrequency.Add(button23, 466);//AIS
            buttonToFrequency.Add(button24, 554);//CIS2
            buttonToFrequency.Add(button25, 622);//DIS2

            //привязка клавиш к клавиатуре
            keyToFrequency.Add(Keys.I, 698);
            keyToFrequency.Add(Keys.U, 659);
            keyToFrequency.Add(Keys.Y, 587);
            keyToFrequency.Add(Keys.T, 523);
            keyToFrequency.Add(Keys.R, 493);
            keyToFrequency.Add(Keys.E, 440);
            keyToFrequency.Add(Keys.W, 392);
            keyToFrequency.Add(Keys.Q, 349);
            keyToFrequency.Add(Keys.M, 329);
            keyToFrequency.Add(Keys.N, 293);
            keyToFrequency.Add(Keys.B, 261);
            keyToFrequency.Add(Keys.V, 247);
            keyToFrequency.Add(Keys.C, 220);
            keyToFrequency.Add(Keys.X, 196);
            keyToFrequency.Add(Keys.Z, 174);
            keyToFrequency.Add(Keys.S, 185);
            keyToFrequency.Add(Keys.D, 207);
            keyToFrequency.Add(Keys.F, 233);
            keyToFrequency.Add(Keys.H, 277);
            keyToFrequency.Add(Keys.J, 311);//цифры начинаются с D
            keyToFrequency.Add(Keys.D2, 369);
            keyToFrequency.Add(Keys.D3, 415);
            keyToFrequency.Add(Keys.D4, 466);
            keyToFrequency.Add(Keys.D6, 554);
            keyToFrequency.Add(Keys.D7, 622);

            // Привязка событий к кнопкам
            foreach (var entry in buttonToFrequency)
            {
                entry.Key.MouseDown += (sender, e) => Beep(entry.Value, 1000);
                entry.Key.MouseUp += (sender, e) => Beep(0, 0);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyToFrequency.ContainsKey(e.KeyCode) && !pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Add(e.KeyCode);

                // Запускаем длительный Beep в отдельном потоке
                Task.Run(() => Beep(keyToFrequency[e.KeyCode], 2000)); // 2 секунды звучания
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
        }
    }
}
