using ChristianJodi.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ChristianJodi.ViewModel
{
    public partial class SettingsViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        readonly IList<ProfilePhoto> source;

        public ObservableCollection<ProfilePhoto> Monkeys { get; private set; }

        public SettingsViewModel()
        {
            source = new List<ProfilePhoto>();
            CreateMonkeyCollection();
        }

        void CreateMonkeyCollection()
        {

            source.Add(new ProfilePhoto
            ("https://upload.wikimedia.org/wikipedia/commons/thumb/0/02/Semnopithèque_blanchâtre_mâle.JPG/192px-Semnopithèque_blanchâtre_mâle.JPG"
            ));

            source.Add(new ProfilePhoto
            ("https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/Gelada-Pavian.jpg/320px-Gelada-Pavian.jpg"
            ));

            Monkeys = new ObservableCollection<ProfilePhoto>(source);
        }
    }
}
