using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SongNames
{
    FundiscoMusicTown, BounceComaMedia, ThePodcastIntroMusicUnlimited, AllIWantEvgenyBardyuzha, ArcadiaGerardoRodriguez, FunkCitySynthwaveFunkBackgroundInstrumentalMigarajay,
    HyronEidunn, PositiveRetrowaveGrooveAntonVlasov, SunsetRiderFASSounds
}

public enum SoundNames
{
    BeamBoink, Boink, CdBounce, DiskScratchBase, DiskScratchRewind, DiskScratchSwaggelicious, Door, DubstepSound, JetEngine1, JetEngine2, MetalImpact, MetalBoink, RobotArm, RobotCounting,
    RoboticVictorySound, RobotLaughing, RobotTransformerSound, RobotDropTheBase, RobotFarewellStranger, RobotIAmARobot, RobotLetsRock, RobotMissionComplete, RobotRadiationMusic, RobotReady,
    RobotRockNRoll, RobotSelfDestruct, RobotSeverDamage, RobotShutdown, RobotSneeze, SpaceSwoosh, VaultDoor
}

[CreateAssetMenu(fileName = "MusicData", menuName = "ScriptableObjects", order = 0)]
public class MusicData : ScriptableObject
{
    [System.Serializable]
    public struct Song
    {
        public SongNames Name;
        public AudioClip SongClip;
        public int BPM;

        public Song(SongNames names, AudioClip song, int bpm)
        {
            this.Name = names;
            this.SongClip = song;
            this.BPM = bpm;
        }
    }
    [System.Serializable]
    public struct Sound
    {
        public SoundNames Name;
        public AudioClip SoundClip;

        public Sound(SoundNames name, AudioClip soundClip)
        {
            Name = name;
            SoundClip = soundClip;
        }
    }

    [SerializeField] public List<Song> Songs;
    [SerializeField] public List<Sound> Sounds;
}
