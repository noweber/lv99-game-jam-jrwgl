public sealed class Rigidbody2dPlayerChaser : Rigidbody2dTargetChaser
{
    protected override void Start()
    {
        base.Start();
        var player = FindObjectOfType<TopDownPlayerController>();
        if (player != null)
        {
            SetTarget(player.gameObject);
        }
    }
}
