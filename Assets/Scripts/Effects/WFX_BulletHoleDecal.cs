using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class WFX_BulletHoleDecal : MonoBehaviour
{
    static private Vector2[] quadUVs = new Vector2[]
    {
        new Vector2(0, 0),
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(1, 1)
    };

    public float lifetime = 10f;
    public float fadeoutpercent = 80;
    public Vector2 frames = new Vector2(1, 1);
    public bool randomRotation = false;
    public bool deactivate = false;

    private float life;
    private float fadeout;
    private Color color;
    private float orgAlpha;
    private Material material;

    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;

        // Пытаемся получить основной цвет из _BaseColor (URP Lit)
        if (material.HasProperty("_BaseColor"))
        {
            color = material.GetColor("_BaseColor");
        }
        else
        {
            Debug.LogWarning("Material has no _BaseColor property. Defaulting to white.");
            color = Color.white;
        }

        orgAlpha = color.a;
    }

    void OnEnable()
    {
        // Безопасное UV-применение
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        if (mesh.vertexCount == 4)
        {
            int random = Random.Range(0, (int)(frames.x * frames.y));
            int fx = (int)(random % frames.x);
            int fy = (int)(random / frames.y);

            Vector2[] meshUvs = new Vector2[4];
            for (int i = 0; i < 4; i++)
            {
                meshUvs[i].x = (quadUVs[i].x + fx) * (1.0f / frames.x);
                meshUvs[i].y = (quadUVs[i].y + fy) * (1.0f / frames.y);
            }

            mesh.uv = meshUvs;
        }
        else
        {
            Debug.LogWarning("Bullet hole decal: Mesh does not have 4 vertices. UV animation skipped.");
        }

        // Случайный поворот
        if (randomRotation)
            transform.Rotate(0f, 0f, Random.Range(0f, 360f), Space.Self);

        // Запуск таймера
        life = lifetime;
        fadeout = life * (fadeoutpercent / 100f);
        color.a = orgAlpha;

        if (material.HasProperty("_BaseColor"))
            material.SetColor("_BaseColor", color);

        StopAllCoroutines();
        StartCoroutine(HoleUpdate());
    }

    IEnumerator HoleUpdate()
    {
        while (life > 0f)
        {
            life -= Time.deltaTime;

            if (life <= fadeout)
            {
                color.a = Mathf.Lerp(0f, orgAlpha, life / fadeout);

                if (material.HasProperty("_BaseColor"))
                    material.SetColor("_BaseColor", color);
            }

            yield return null;
        }

        if (deactivate)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}