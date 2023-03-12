// ReSharper disable once CheckNamespace
namespace QFX.IFX
{
    public class IFX_SelfDestroyer : IFX_ControlledObject
    {
        public float LifeTime;


        private void Start()
        {
            Destroy(this.gameObject, LifeTime);
        }

        /*
        public override void Run()
        {
            base.Run();
            Destroy(gameObject, LifeTime);
        }
        */
    }
}