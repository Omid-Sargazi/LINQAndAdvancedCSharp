namespace AlgorithemInCSharp.Patterns
{
    public interface IDevice
    {
        bool IsEnable { get; }
        void Enable();
        void Disable();
        void SetVolume(int percent);
        void SetChannel(int channel);
    }

    public class Tv : IDevice
    {
        private bool _isOn = false;
        private int _volume = 50;
        private int _channel = 3;
        public bool IsEnable => _isOn;

        public void Disable()
        {
            _isOn = false;
            Console.WriteLine("TV is now Off.");
        }

        public void Enable()
        {
            _isOn = true;
            Console.WriteLine("TV is now ON.");
        }

        public void SetChannel(int channel)
        {
            _channel = channel;
            Console.WriteLine($"TV Channel Set to {_channel}.");
        }

        public void SetVolume(int percent)
        {
            _volume = percent;
            Console.WriteLine($"TV Volume set to {_volume}%");
        }
    }

    public class Radio : IDevice
    {
        private bool _isOn = false;
        private int _volume = 30;
        private int _frequency = 880;
        public bool IsEnable => _isOn;

        public void Disable()
        {
            _isOn = false;
            Console.WriteLine("Radio is now Off.");
        }

        public void Enable()
        {
            _isOn = true;
            Console.WriteLine("Radio is now ON.");
        }

        public void SetChannel(int channel)
        {
            _frequency = channel;
            Console.WriteLine($"Radio frequency set to {_frequency} AM.");
        }

        public void SetVolume(int percent)
        {
            _volume = percent;
            Console.WriteLine($"Radio volume set to {_volume}%.");

        }
    }

    public abstract class AbstractRemote
    {
        protected readonly IDevice _device;
        public AbstractRemote(IDevice device)
        {
            _device = device;
        }

        public abstract void TogglePower();
        public abstract void VolumeUp();
        public abstract void VolumeDown();
        public abstract void ChannelUp();
        public abstract void ChannelDown();
    }

    public class BasicRemote : AbstractRemote
    {
        public BasicRemote(IDevice device) : base(device)
        {
        }

        public override void ChannelDown()
        {
            throw new NotImplementedException();
        }

        public override void ChannelUp()
        {
            throw new NotImplementedException();
        }

        public override void TogglePower()
        {
            throw new NotImplementedException();
        }

        public override void VolumeDown()
        {
            throw new NotImplementedException();
        }

        public override void VolumeUp()
        {
            throw new NotImplementedException();
        }
    }

    public class AdvancedRemote : AbstractRemote
    {
        public AdvancedRemote(IDevice device) : base(device)
        {
        }

        public override void ChannelDown()
        {
            throw new NotImplementedException();
        }

        public override void ChannelUp()
        {
            throw new NotImplementedException();
        }

        public override void TogglePower()
        {
            throw new NotImplementedException();
        }

        public override void VolumeDown()
        {
            throw new NotImplementedException();
        }

        public override void VolumeUp()
        {
            throw new NotImplementedException();
        }
    }
}