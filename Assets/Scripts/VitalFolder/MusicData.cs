using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SongNames
{
    FundiscoMusicTown, BounceComaMedia, ThePodcastIntroMusicUnlimited, AllIWantEvgenyBardyuzha, ArcadiaGerardoRodriguez, FunkCitySynthwaveFunkBackgroundInstrumentalMigarajay,
    HyronEidunn, PositiveRetrowaveGrooveAntonVlasov, SunsetRiderFASSounds
}

[CreateAssetMenu(fileName = "MusicData", menuName = "ScriptableObjects", order = 0)]
public class MusicData : ScriptableObject
{
    [System.Serializable]
    public struct Song
    {
        public SongNames names;
        public AudioClip song;
        public int bpm;

        public Song(SongNames names, AudioClip song, int bpm)
        {
            this.names = names;
            this.song = song;
            this.bpm = bpm;
        }
    }

    [SerializeField] public List<Song> songs;
}
