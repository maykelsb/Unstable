using UnityEngine;

public class DamageableEntity : MonoBehaviour
{
    public enum EffectType { Monster, Hero, Poison };
    private EffectType effectType;

    private float duration = .75f;
    private float interval = 0.0625f;

    private Color tintedColor; //@TODO move to hittable?
    private bool isTinted = false;

    protected SpriteRenderer sptRenderer;
    protected Stats stats;

    public void Start()
    {
        sptRenderer = GetComponent<SpriteRenderer>();
        stats = GetComponent<Stats>();
    }

    protected DamageableEntity ConfigEffect(EffectType effect, float interval)
    {
        this.interval = interval;
        ConfigEffect(effect);
        return this;
    }

    protected DamageableEntity ConfigEffect(EffectType effect)
    {
        switch (effect)
        {
            case EffectType.Hero:
                tintedColor = new Color(1, 1, 1, .25f);
                break;
            case EffectType.Monster:
                tintedColor = new Color(1, 0, 0, 1);
                break;
            case EffectType.Poison:
                tintedColor = new Color(0, 1, 0, 1);
                break;
        }
        return this;
    }

    public void TakeDamage(GameObject agressor)
    {
        stats.life -= agressor.GetComponent<Stats>()
            .CalculateDamage(stats.defense);
        StartDamageEffect();
    }

    public void TakeDamageFromTrap(TrapStats trap)
    {
        trap.Spring();
        stats.life -= trap.GetDamage();
    }

    protected DamageableEntity StartDamageEffect(float duration)
    {
        this.duration = duration;
        StartDamageEffect();

        return this;
    }

    protected DamageableEntity StartDamageEffect()
    {
        StopDamageEffect();
        InvokeRepeating("DamageEffect", 0.0f, interval);
        Invoke("StopDamageEffect", duration);

        return this;
    }

    protected void DamageEffect()
    {
        if (isTinted)
        {
            sptRenderer.color = new Color(1, 1, 1, 1);
            isTinted = false;
            return;
        }
        sptRenderer.color = tintedColor;
        isTinted = true;
    }

    protected void StopDamageEffect()
    {
        isTinted = false;
        sptRenderer.color = new Color(1, 1, 1, 1);
        CancelInvoke("DamageEffect");
    }

    protected bool AmIHero()
    {
        return gameObject.CompareTag("Hero");
    }

    public bool AmIEnemy()
    {
        return gameObject.CompareTag("Enemy");
    }
}
