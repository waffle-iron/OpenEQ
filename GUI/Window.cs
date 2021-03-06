﻿using ImGuiNET;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using static System.Console;

namespace OpenEQ.GUI {
    [MoonSharpUserData]
    public class Window : BaseWidget {
        public string Title;
        public bool Closable = false;
        List<IWidget> widgets;
        bool open = true;
        public bool Open {
            get { return Visible && (open || !Closable); }
            set { open = value; }
        }

        public Window(string title) {
            Title = title;
            widgets = new List<IWidget>();
        }

        public T Add<T>(T widget) where T : IWidget {
            widgets.Add(widget);
            widget.ParentWindow = this;
            return widget;
        }

        public Button CreateButton(string label) {
            return Add(new Button(label));
        }
        public Textbox CreateTextbox(string label = "", int maxLength = 256) {
            return Add(new Textbox(label, maxLength));
        }
        public Label CreateLabel(string text) {
            return Add(new Label(text));
        }
        public Label CreateLabel(Func<string> update) {
            return Add(new Label(update));
        }

        public override void Render() {
            if(Open) {
                ImGui.GetStyle().WindowRounding = 10;
                ImGui.BeginWindow(Title, ref open, WindowFlags.AlwaysAutoResize);
                ImGuiNative.igSetWindowFontScale(2.0f);

                foreach(var widget in widgets)
                    widget.Render();

                ImGui.EndWindow();
            }
        }
    }
}
