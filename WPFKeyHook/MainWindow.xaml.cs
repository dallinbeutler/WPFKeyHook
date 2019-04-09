
using System;
using System.Windows;

//using EventHook;
using Gma.System.MouseKeyHook;
using System.Collections.Generic;



namespace WPFKeyHook
{
    /// <summary>s
    /// Interaction logic for MainWindow.xamlb
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
                InitializeComponent();

            //1. Define key combinations
            var undo = Combination.FromString("RMenu+J");
            
            var fullScreen = Combination.FromString("Shift+Alt+Enter");

            //2. Define actions
            Action actionUndo = DoSomething;
            Action actionFullScreen = () => { Console.WriteLine("You Pressed FULL SCREEN"); };

            

                        //3. Assign actions to key combinations
             var assignment = new Dictionary<Combination, Action>
            {
                {undo, actionUndo},
                {fullScreen, actionFullScreen}
            };

            //4. Install listener
            Hook.GlobalEvents().OnCombination(assignment);
            Hook.GlobalEvents().KeyDown += MainWindow_KeyDown;

            while (true)
            {

            }
            
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool PaintDesktop(IntPtr hdc);

        private void Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            IntPtr HDC = e.Graphics.GetHdc(); //Get the device context
            PaintDesktop(HDC); //Paint the form with the desktop background
            e.Graphics.DrawEllipse(new System.Drawing.Pen(System.Drawing.Color.Black), 10, 10, 100, 100);

            e.Graphics.ReleaseHdc(HDC); //Release the device context
        }
        private void MainWindow_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.KeyValue.ToString());
            System.Windows.Forms.SendKeys.Send("f");
            //System.Diagnostics.Process.

        }

        void DoSomething()
        {
            Console.WriteLine("You pressed UNDO");
        }



        private void Mhooks_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = true;

            MessageBox.Show(e.KeyChar.ToString());
               
        }

        //private void KeyboardWatcher_OnKeyInput(object sender, KeyInputEventArgs e)
        //{
        //    MessageBox.Show(string.Format("Key {0} event of key {1}", e.KeyData.EventType, e.KeyData.Keyname));
            
        //}
    }

}
