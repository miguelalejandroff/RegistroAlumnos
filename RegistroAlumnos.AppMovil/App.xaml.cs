﻿using RegistroAlumnos.AppMovil.Views;

namespace RegistroAlumnos.AppMovil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
