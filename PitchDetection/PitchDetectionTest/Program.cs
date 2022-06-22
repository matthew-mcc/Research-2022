

var waveIn = new NAudio.Wave.WaveInEvent{
    DeviceNumber = 0,
    WaveFormat = new NAudio.Wave.WaveFormat(rate: 44100, bits: 16, channels: 1),
    BufferMilliseconds = 20
};

waveIn.DataAvailable += ShowPeakMono;
waveIn.StartRecording();

static void ShowPeakMono(object sender, NAudio.Wave.WaveInEventArgs args){
    float maxvalue = 32767;
    int peakValue = 0;
    int bytesPerSample = 2;
    for (int index = 0; index < args.BytesRecorded; index+= bytesPerSample){
        int value = BitConverter.ToInt16(args.Buffer, index);
        peakValue = Math.Max(peakValue, value);
    }

    Console.WriteLine("L=" + GetBars(peakValue / maxvalue));
}

static string GetBars(double fraction, int barCount = 35){
    int barsOn = (int) (barCount * fraction);
    int barsOff = barCount - barsOn;
    return new string('#', barsOn) + new string('-', barsOff);
}