using TaleWorlds.CampaignSystem;
using TaleWorlds.GauntletUI;

namespace BetterTime
{
    public class TimePanel : Widget
    {
        private int _currenTimeState;
        private ButtonWidget _fastForwardButton;
        private ButtonWidget _fastFastForwardButton;
        private ButtonWidget _playButton;
        private ButtonWidget _pauseButton;

        public TimePanel(UIContext context)
            : base(context)
        {
            AddState("Disabled");
        }

        protected override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);
            if (IsDisabled)
            {
                SetState("Disabled");
            }
            else
            {
                if (Campaign.Current != null)
                {
                    var fffc = FastFastForwardButton.ClickEventHandlers;
                    var ffc = FastForwardButton.ClickEventHandlers;
                    var plc = PlayButton.ClickEventHandlers;
                    var pac = PauseButton.ClickEventHandlers;
                    if (fffc.Count == 0)
                    {
                        fffc.Add((a) => { Campaign.Current.SpeedUpMultiplier = 8f; });
                    }
                    if (ffc.Count == 0)
                    {
                        ffc.Add((a) => { Campaign.Current.SpeedUpMultiplier = 4f; });
                    }
                    if (plc.Count == 0)
                    {
                        plc.Add((a) => { Campaign.Current.SpeedUpMultiplier = 4f; });
                    }
                    if (pac.Count == 0)
                    {
                        pac.Add((a) => { Campaign.Current.SpeedUpMultiplier = 4f; });
                    }
                }

                SetState("Default");
                PlayButton.IsSelected = false;
                FastFastForwardButton.IsSelected = false;
                FastForwardButton.IsSelected = false;
                PauseButton.IsSelected = false;
                switch (CurrentTimeState)
                {
                    case 0:
                    case 6:
                        PauseButton.IsSelected = true;
                        break;
                    case 1:
                    case 3:
                        PlayButton.IsSelected = true;
                        break;
                    case 2:
                    case 4:
                    case 5:
                        if (Campaign.Current.SpeedUpMultiplier == 8f)
                        {
                            FastFastForwardButton.IsSelected = true;
                        }
                        else
                        {
                            FastForwardButton.IsSelected = true;
                        }
                        break;
                }
            }
        }

        [Editor(false)]
        public int CurrentTimeState
        {
            get
            {
                return this._currenTimeState;
            }
            set
            {
                if (this._currenTimeState == value)
                    return;
                this._currenTimeState = value;
                this.OnPropertyChanged((object)value, nameof(CurrentTimeState));
            }
        }


        [Editor(false)]
        public ButtonWidget FastFastForwardButton
        {
            get
            {
                return this._fastFastForwardButton;
            }
            set
            {
                if (this._fastFastForwardButton == value)
                    return;
                this._fastFastForwardButton = value;
                this.OnPropertyChanged((object)value, nameof(FastFastForwardButton));
            }
        }


        [Editor(false)]
        public ButtonWidget FastForwardButton
        {
            get
            {
                return this._fastForwardButton;
            }
            set
            {
                if (this._fastForwardButton == value)
                    return;
                this._fastForwardButton = value;
                this.OnPropertyChanged((object)value, nameof(FastForwardButton));
            }
        }

        [Editor(false)]
        public ButtonWidget PlayButton
        {
            get
            {
                return this._playButton;
            }
            set
            {
                if (this._playButton == value)
                    return;
                this._playButton = value;
                this.OnPropertyChanged((object)value, nameof(PlayButton));
            }
        }

        [Editor(false)]
        public ButtonWidget PauseButton
        {
            get
            {
                return this._pauseButton;
            }
            set
            {
                if (this._pauseButton == value)
                    return;
                this._pauseButton = value;
                this.OnPropertyChanged((object)value, nameof(PauseButton));
            }
        }
    }
}
