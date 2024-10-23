using LiteEntitySystem.Collections;
using LiteEntitySystem.Transport;

namespace LiteEntitySystem
{
    public class NetPlayer
    {
        public readonly byte Id;
        public readonly AbstractNetPeer Peer;
        
        internal ushort LastProcessedTick;
        internal ushort LastReceivedTick;
        internal ushort CurrentServerTick;
        internal ushort StateATick;
        internal ushort StateBTick;
        internal float LerpTime;
        
        //server only
        internal NetPlayerState State;
        internal readonly SequenceBinaryHeap<InputBuffer> AvailableInput = new (ServerEntityManager.MaxStoredInputs);

        private int _visibilityLayer;

        /// <summary>
        /// Visibility filter allows this entity to only be visible to players within the same visibility filter
        /// 0 is a special value meaning it's visible to all players
        /// </summary>
        public int VisibilityLayer
        {
            get
            {
                return _visibilityLayer;
            }
            set
            {
                if (_visibilityLayer != value)
                {
                    PreviousVisibilityLayer = _visibilityLayer;
                    _visibilityLayer = value;
                    VisibilityLayerChanged = true;
                }
            }
        }
        internal bool VisibilityLayerChanged;
        internal int PreviousVisibilityLayer;

        internal NetPlayer(AbstractNetPeer peer, byte id)
        {
            Id = id;
            Peer = peer;
        }
    }
}