using Content.Shared.Damage;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.Projectiles;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ProjectileComponent : Component
{
    /// <summary>
    ///     The angle of the fired projectile.
    /// </summary>
    [DataField, AutoNetworkedField]
    public Angle Angle;

    /// <summary>
    ///     The effect that appears when a projectile collides with an entity.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public EntProtoId? ImpactEffect;

    /// <summary>
    ///     User that shot this projectile.
    /// </summary>
    [DataField, AutoNetworkedField]
    public EntityUid? Shooter;

    /// <summary>
    ///     Weapon used to shoot.
    /// </summary>
    [DataField, AutoNetworkedField]
    public EntityUid? Weapon;

    /// <summary>
    ///     The projectile spawns inside the shooter most of the time, this prevents entities from shooting themselves.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool IgnoreShooter = true;

    /// <summary>
    ///     Prevent hitting anything on shooter grid. Useful for ship weapons.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool IgnoreWeaponGrid = false;

    /// <summary>
    ///     The amount of damage the projectile will do.
    /// </summary>
    [DataField(required: true)] [ViewVariables(VVAccess.ReadWrite)]
    public DamageSpecifier Damage = new();

    /// <summary>
    ///     If the projectile should be deleted on collision.
    /// </summary>
    [DataField]
    public bool DeleteOnCollide = true;

    /// <summary>
    ///     Ignore all damage resistances the target has.
    /// </summary>
    [DataField]
    public bool IgnoreResistances = false;

    /// <summary>
    ///     Get that juicy FPS hit sound.
    /// </summary>
    [DataField]
    public SoundSpecifier? SoundHit;

    /// <summary>
    ///     Force the projectiles sound to play rather than potentially playing the entity's sound.
    /// </summary>
    [DataField]
    public bool ForceSound = false;

    /// <summary>
    ///     Whether this projectile will only collide with entities if it was shot from a gun (if <see cref="Weapon"/> is not null).
    /// </summary>
    [DataField]
    public bool OnlyCollideWhenShot = false;

    /// <summary>
    ///     Whether this projectile has already damaged an entity.
    /// </summary>
    [DataField]
    public bool DamagedEntity;

    // Goobstation start
    [DataField]
    public bool Penetrate;

    [NonSerialized]
    public List<EntityUid> IgnoredEntities = new();
    // Goobstation end

    // Hullrot start
    // how many points of armor can this ammunition ignore SPCR 2025
    [DataField("harmorPenetration")]
    public float HullrotArmorPenetration = 0;

    // stamina damage that ignores any armor/buff/etc SPCR 2025
    [DataField]
    public float stoppingPower = 0;

    /// <summary>
    ///  modifies some behaviour regarding fixture checks for the projectile system, set by phasePreventSystem, SPCR 2025
    /// </summary>
    public bool raycasting = false;
    // Hullrot end
}
