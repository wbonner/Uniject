using Ninject;
using System;
using Testable;

/// <summary>
/// A bouncy sphere containing a light, that changes its colour when it hits something.
/// </summary>
public class BouncingLight : TestableComponent {
    
    private ILight light;
    private Random rand;
    public const float killThresholdY = -5.0f;

    public BouncingLight(TestableGameObject obj,
                         Sphere sphere,
                         [Resource("physic/bouncy")] IPhysicMaterial material,
                         ILight light,
                         Random rand) : base(obj) {
        this.light = light;
        this.rand = rand;
        sphere.collider.material = material;
        light.intensity = 2.0f;
        light.range = 15;
    }

    public override void Update() {
        if (this.Obj.transform.Position.y < killThresholdY) {
            Obj.Destroy();
        }
    }
    
    public override void OnCollisionEnter(Collision collision) {
        light.color = new UnityEngine.Color((float) rand.NextDouble(), (float) rand.NextDouble(), (float) rand.NextDouble());
    }
}

/// <summary>
/// Main scene class, instantiates a box to represent the floor and randomly spawns <c>BouncingLight</c>s.
/// </summary>
public class TestableCollisions {

    private IKernel kernel;
    private Random rand;

    public TestableCollisions(UnijectUtil.IntervalBasedCallback caller,
                              [GameObjectBoundary] Box box,
                              IKernel kernel,
                              Random rand) {
        this.kernel = kernel;
        this.rand = rand;

        caller.callback = onSpawn;
        caller.interval = new TimeSpan(0, 0, 3);
        box.Obj.transform.localScale = new UnityEngine.Vector3(50, 1, 50);
    }

    private void onSpawn() {
        BouncingLight light = kernel.Get<BouncingLight>();
        light.Obj.transform.Translate(new UnityEngine.Vector3((float) rand.NextDouble(), 4 + (float) rand.NextDouble() * 10.0f, (float) rand.NextDouble()));
    }
}
