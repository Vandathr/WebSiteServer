using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSiteServer
{
    internal class Visual: Form
    {
        private readonly Model refToModel;
        private readonly ListBox Messagez = new ListBox();
        private readonly Size ScreenSize = new Size(400, 400);
        private readonly Button[] buttonN = new Button[3];
        private readonly Size StandardButtonSize = new Size(150, 30);
        private readonly int standardDistance = 5;


        public Visual(Model refToModel)
        {
            this.refToModel = refToModel;

            var align = standardDistance;
            var floor = standardDistance;


            Messagez.DataSource = refToModel.GetMessagezList();
            Positioner.LocateControl(Messagez, ScreenSize, new Point());
            Controls.Add(Messagez);

            Text = "HTTP Server";
            Width = 1400;
            Height = 750;

            for (int i = 0; i < buttonN.Length; i++)
                buttonN[i] = new Button();

            floor = Messagez.Bottom + standardDistance;

            ConfigureButton(startButtonIndex, align, floor, "Запустить сервер");

            align = buttonN[startButtonIndex].Right + standardDistance;

            ConfigureButton(stopButtonIndex, align, floor, "Остановить сервер");

            align = standardDistance;
            floor = buttonN[startButtonIndex].Bottom + standardDistance;

            ConfigureButton(reloadButtonIndex, align, floor, "Перезагрузить файлы");

            foreach (var e in buttonN)
            Controls.Add(e);


            buttonN[startButtonIndex].Click += (sender, args) => refToModel.LaunchServer();
            buttonN[stopButtonIndex].Click += (sender, args) => refToModel.CeaseCerver();
            buttonN[reloadButtonIndex].Click += (sender, args) => refToModel.ReloadData();

        }

        private void ConfigureButton(int buttonIndex, int align, int floor, string buttonText)
        {
            Positioner.LocateControl(buttonN[buttonIndex], StandardButtonSize, new Point(align, floor));
            buttonN[buttonIndex].Text = buttonText;
        }


        private readonly static int startButtonIndex = 0;
        private readonly static int stopButtonIndex = 1;
        private readonly static int reloadButtonIndex = 2;


    }
}
