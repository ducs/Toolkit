
using System;
using Toolkit.Core.Interface.Mvvm.ViewModels;

namespace Toolkit.Presets
{
    public class PresetManager : ViewModelBase
    {
        internal const string DefaultPreset = "Default";

        private string _colorPreset = DefaultPreset;
        private string _shapePreset = DefaultPreset;

        private PresetManager()
        {
        }

        public static PresetManager Current { get; } = new PresetManager();

        public string ColorPreset
        {
            get => _colorPreset;
            set { SetProperty(ref _colorPreset, value); }
        }

        public string ShapePreset
        {
            get => _shapePreset;
            set { SetProperty(ref _shapePreset, value); }
            
        }

        public event EventHandler ColorPresetChanged;
        public event EventHandler ShapePresetChanged;
    }
}
