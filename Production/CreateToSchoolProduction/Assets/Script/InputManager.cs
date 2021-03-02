using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public float Threshold { get; private set; }

    public Dictionary<Button, ButtonState> Buttons;
    public Dictionary<Axis, AxisState> Axises;

    /*static Dictionary<Button, ButtonState> defaultButtons = new Dictionary<Button, ButtonState>()
    {
        {Button.A ,new ButtonState(Button.A, KeyCode.Z, KeyCode.JoystickButton0) },
        {Button.B ,new ButtonState(Button.B, KeyCode.C, KeyCode.JoystickButton1) },
        {Button.X ,new ButtonState(Button.X, KeyCode.X, KeyCode.JoystickButton2) },
        {Button.Y ,new ButtonState(Button.Y, KeyCode.V, KeyCode.JoystickButton3) },
        {Button.Up ,new ButtonState(Button.Up, KeyCode.W,  AxisName[6],true) },
        {Button.Down ,new ButtonState(Button.Down, KeyCode.S,  AxisName[6],false) },
        {Button.Left ,new ButtonState(Button.Left, KeyCode.A,  AxisName[5],true) },
        {Button.Right ,new ButtonState(Button.Right, KeyCode.D,  AxisName[5],false) },
        {Button.Ljoy ,new ButtonState(Button.Right, KeyCode.LeftArrow, KeyCode.JoystickButton8) },
        {Button.LT ,new ButtonState(Button.LT, KeyCode.Q, AxisName[2],true) },
        {Button.LB ,new ButtonState(Button.LB, KeyCode.Alpha1, KeyCode.JoystickButton4) },
        {Button.Rjoy ,new ButtonState(Button.Right, KeyCode.RightArrow, KeyCode.JoystickButton9) },
        {Button.RT ,new ButtonState(Button.RT, KeyCode.E, AxisName[2],false) },
        {Button.RB ,new ButtonState(Button.RB, KeyCode.Alpha3, KeyCode.JoystickButton5) },
        {Button.Start ,new ButtonState(Button.Start, KeyCode.F, KeyCode.JoystickButton6) },
        {Button.Back ,new ButtonState(Button.Back, KeyCode.G, KeyCode.JoystickButton7) }
    };*/

    void Awake()
    {
        Buttons = new Dictionary<Button, ButtonState>()
            {
                {Button.A ,new ButtonState(Button.A, KeyCode.Z, KeyCode.JoystickButton0) },
                {Button.B ,new ButtonState(Button.B, KeyCode.C, KeyCode.JoystickButton1) },
                {Button.X ,new ButtonState(Button.X, KeyCode.X, KeyCode.JoystickButton2) },
                {Button.Y ,new ButtonState(Button.Y, KeyCode.V, KeyCode.JoystickButton3) },
                {Button.Up ,new ButtonState(Button.Up, KeyCode.W,  AxisName[6],true) },
                {Button.Down ,new ButtonState(Button.Down, KeyCode.S,  AxisName[6],false) },
                {Button.Left ,new ButtonState(Button.Left, KeyCode.A,  AxisName[5],true) },
                {Button.Right ,new ButtonState(Button.Right, KeyCode.D,  AxisName[5],false) },
                {Button.Ljoy ,new ButtonState(Button.Right, KeyCode.LeftArrow, KeyCode.JoystickButton8) },
                {Button.LT ,new ButtonState(Button.LT, KeyCode.Q, AxisName[2],true) },
                {Button.LB ,new ButtonState(Button.LB, KeyCode.Alpha1, KeyCode.JoystickButton4) },
                {Button.Rjoy ,new ButtonState(Button.Right, KeyCode.RightArrow, KeyCode.JoystickButton9) },
                {Button.RT ,new ButtonState(Button.RT, KeyCode.E, AxisName[2],false) },
                {Button.RB ,new ButtonState(Button.RB, KeyCode.Alpha3, KeyCode.JoystickButton5) },
                {Button.Start ,new ButtonState(Button.Start, KeyCode.F, KeyCode.JoystickButton6) },
                {Button.Back ,new ButtonState(Button.Back, KeyCode.G, KeyCode.JoystickButton7) }
            };
    }

    void Update()
    {
        foreach (ButtonState button in Buttons.Values)
        {
            if (button != null && button.SetState)
            {
                button.Update();
            }
        }
    }

    public bool OnButtonDown(Button button)
    {
        return Buttons[button].OnKeyDown();
    }

    public bool OnButtonStay(Button button)
    {
        return Buttons[button].OnKeyStay();
    }

    public bool OnButtonUp(Button button)
    {
        return Buttons[button].OnKeyUp();
    }

    public bool OnAnyButtonDown()
    {
        for(int i =0;i<(int)Button.Null;i++)
        {
            if (Buttons[(Button)i].OnKeyDown()) return true;
        }
        return false;
    }

    public bool OnAnyButtonStay()
    {
        for (int i = 0; i < (int)Button.Null; i++)
        {
            if (Buttons[(Button)i].OnKeyStay()) return true;
        }
        return false;
    }
    public bool OnAnyButtonUp()
    {
        for (int i = 0; i < (int)Button.Null; i++)
        {
            if (Buttons[(Button)i].OnKeyUp()) return true;
        }
        return false;
    }
    public float StatTime(Button button)
    {
        return Buttons[button].StayTime;
    }

    public void CreateButton(ButtonState state)
    {

    }

    /// <summary>
    /// Buttonとしての入力管理部分
    /// </summary>
    [System.Serializable]
    public class ButtonState
    {
        public Button button { get; private set; }
        string name;            // Editor拡張orコンフィグ作るときの名前
        KeyCode code;           // コントローラー入力のKeyCode
        KeyCode key;            // キーボード入力のKeyCode
        string Axis_RLT;        // 致し方無いLB,RB用のAxisstring
        bool Positive;          // 致し方無いLB,RB用の区別
        bool previous;          // １つ前のflame
        bool now;               // 現在のflame
        public bool SetState { get; private set; }
        public float StayTime { get; private set; }

        public ButtonState(Button _button)
        {
            button = _button;
            SetState = true;
        }

        public ButtonState(Button _button, KeyCode _key)
        {
            button = _button;
            key = _key;
            SetState = true;
        }
        public ButtonState(Button _button, KeyCode _key, KeyCode _code)
        {
            button = _button;
            key = _key;
            code = _code;
            SetState = true;
        }

        public ButtonState(Button _button, string _AxisName, bool _posi)
        {
            button = _button;
            Axis_RLT = _AxisName;
            Positive = _posi;
            SetState = true;
        }
        public ButtonState(Button _button,KeyCode _key, string _AxisName, bool _posi)
        {
            button = _button;
            key = _key;
            Axis_RLT = _AxisName;
            Positive = _posi;
            SetState = true;
        }

        public void Update()
        {
            previous = now;
            if (!string.IsNullOrEmpty(Axis_RLT))
            {
                now = Math.Abs(Input.GetAxis(Axis_RLT)) > 0 && Input.GetAxis(Axis_RLT) > 0 == Positive;
            }
            else
            {
                now = Input.GetKey(key) || Input.GetKey(code);
            }

            StayTime = now && previous ? StayTime + Time.deltaTime : 0;
        }

        public bool OnKeyDown()
        {
            return now && !previous;
        }

        public bool OnKeyStay()
        {
            return now && previous;
        }

        public bool OnKeyUp()
        {
            return !now && previous;
        }
    }

    /// <summary>
    /// Axisとしての入力管理部分
    /// </summary>
    [System.Serializable]
    public class AxisState
    {
        public Axis axis { get; private set; }
        string name;
        string Horizontal_Axis;
        string Vertical_Axis;
        KeyCode[] Positive_Axis;
        KeyCode[] Negative_Axis;
        float previous;
        float now;
        public double Threshold;

    }

    public enum Button
    {
        A, B, X, Y, Up, Down, Left, Right,Ljoy, LT, LB,Rjoy, RT, RB, Start, Back, Null
    }

    public enum Axis
    {
        R_Horizontal, R_Vertical, L_Horizontal, L_Vertical, Null
    }

    public static string[] AxisName = new string[]
    {"Axis 1","Axis 2","Axis 3","Axis 4","Axis 5","Axis 6","Axis 7","Axis 8","Axis 9","Axis 10" };
}
