namespace TrainerAvatarSimulator.Core
{
    public enum AvatarPostureState
    {
        Idle,
        Moving,
        Aligning,
        Standing,
        Sitting,
        Kneeling,
        Lying,
        Exercising,
        HoldingPose,
        WardrobeChange,
        Recovering,
        Negotiating,
        Sanctioned
    }

    public enum CommandType
    {
        Exercise,
        AtomicPosture,
        Wardrobe,
        Discipline
    }

    public enum InteractionType
    {
        None,
        Wall,
        Bench,
        Mat
    }

    public enum ClothingSlot
    {
        Top,
        Bottom,
        Shoes,
        Socks
    }

    public enum SanctionType
    {
        None,
        DoNotCountRep,
        RepeatExercise,
        ExtendHold,
        ExtraRepetitions,
        ShortenRest,
        ExtraSet
    }

    public enum CommandExecutionStatus
    {
        None,
        Pending,
        Running,
        Interrupted,
        Completed,
        Failed
    }

    public enum CommandSource
    {
        Unknown,
        DebugUI,
        GameplayUI,
        System
    }
}
