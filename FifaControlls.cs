﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FifaMulti_v1
{
    public class FifaControlls
    {
        public static UInt32 StatusRegister = 0x00;
        public static int[] getSendCode;
        public enum Buttons
        {
            Left=4,
            Up=1,
            Right=2,
            Down=3,
            RUp=5,
            RRight=6,
            RDown=7,
            RLeft=8,
            A=9,B=10,
            X=11,Y=12,
            L=13,R=14,

            Back = 15, Start = 16,
            LB=17,LT=18,
            RB=19,RT=20,
            ZUp = 21,
            ZRight =22,
            ZDown = 23,
            ZLeft=24
        }
        public FifaControlls()
        {
          



            getSendCode = new int[256];
            getSendCode = Enumerable.Repeat(+0, 256).ToArray();
       
            for (int i = 1; i < 25; i++)
            {
                StringBuilder data = new StringBuilder();
                try
                {
                    Form1.GetPrivateProfileString("KeyMap", "Button_" + i, "", data, 255, ".\\ButtonData.ini");
                    getSendCode[(int)Enum.Parse(typeof(Keys), data.ToString())] = i;
                }
                catch { }
            }
        }
        public static void updateFifaControls(UInt32 data)
        {
            short position=1;
            while (position != 26)// &&  data+1 >= (0x01 << (position - 1)))
            {
                short value = (short)((data & (0x01 << (position - 1))) >> (position - 1));
                switch (position)
                {
                   

                    case (short)Buttons.Left:
                        Form1.m_vjoy.SetXAxis(0, (short)(short.MinValue*value));
                        break;
                    case (short)Buttons.Right:
                        Form1.m_vjoy.SetXAxis(0, (short)(short.MaxValue * value));
                        break;
                    case (short)Buttons.Up:
                        Form1.m_vjoy.SetYAxis(0, (short)(short.MinValue * value));
                        break;
                    case (short)Buttons.Down:
                        Form1.m_vjoy.SetYAxis(0, (short)(short.MaxValue * value));
                        break;
                    case (short)Buttons.RUp:
                        Form1.m_vjoy.SetPOV(0, 0, value!=0?VirtualJoy.POVType.Up:VirtualJoy.POVType.Nil);
                        break;
                    case (short)Buttons.RDown:
                        Form1.m_vjoy.SetPOV(0, 0, value!=0? VirtualJoy.POVType.Down:VirtualJoy.POVType.Nil);
                        break;
                    case (short)Buttons.RLeft:
                        Form1.m_vjoy.SetPOV(0, 0, value!=0? VirtualJoy.POVType.Left:VirtualJoy.POVType.Nil);
                        break;
                    case (short)Buttons.RRight:
                        Form1.m_vjoy.SetPOV(0, 0, value != 0 ? VirtualJoy.POVType.Right : VirtualJoy.POVType.Nil);
                        break;

                    case (short)Buttons.A:
                    case (short)Buttons.B:
                    case (short)Buttons.X:
                    case (short)Buttons.Y:
                    case (short)Buttons.L:
                    case (short)Buttons.R:
                    case (short)Buttons.Back:
                    case (short)Buttons.Start:
                    case (short)Buttons.LB:
                    case (short)Buttons.LT:
                    case (short)Buttons.RB:
                    case (short)Buttons.RT:
                    case (short)Buttons.ZUp:
                    case (short)Buttons.ZRight:
                    case (short)Buttons.ZDown:
                    case (short)Buttons.ZLeft:
        
                        Form1.m_vjoy.SetButton(0, position-9, value!=0?true:false);
                        break;


                     
        
                }
                position++;
            }
            Form1.m_vjoy.Update(0);
            Form1.m_vjoy.Update(1);

        }
       
    }
}
