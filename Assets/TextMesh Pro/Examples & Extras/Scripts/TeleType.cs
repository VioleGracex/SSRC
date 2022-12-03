using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    public class TeleType : MonoBehaviour
    {

        public bool CR_running = true;
        //[Range(0, 100)]
        //public int RevealSpeed = 50;

        //private string label01 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=1>";
        // private string label02 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=2>";


        private TMP_Text m_textMeshPro;

        public bool Cr()
        {
            return CR_running;
        }
        void Awake()
        {
            // Get Reference to TextMeshPro Component
            m_textMeshPro = GetComponent<TMP_Text>();
            //m_textMeshPro.text = label01;
            m_textMeshPro.enableWordWrapping = false;
            //m_textMeshPro.alignment = TextAlignmentOptions.Top;


            StartCoroutine(type());
            //if (GetComponentInParent(typeof(Canvas)) as Canvas == null)
            //{
            //    GameObject canvas = new GameObject("Canvas", typeof(Canvas));
            //    gameObject.transform.SetParent(canvas.transform);
            //    canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

            //    // Set RectTransform Size
            //    gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 300);
            //    m_textMeshPro.fontSize = 48;
            //}


        }

        public void Reveal()
        {
            if(!CR_running)
            {
                StartCoroutine(type());
            }
           else
            {
                m_textMeshPro.maxVisibleCharacters = m_textMeshPro.textInfo.characterCount;
            }
        }

        IEnumerator type()
        {

            // Force and update of the mesh to get valid information.
            m_textMeshPro.ForceMeshUpdate();
            CR_running = true;

            int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount; // Get # of Visible Character in text object
            int counter = 0;
            int visibleCount = 0;

            while (totalVisibleCharacters > visibleCount)
            {
                visibleCount = counter % (totalVisibleCharacters + 1);

                m_textMeshPro.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

                // Once the last character has been revealed, wait 1.0 second and start over.
                if (visibleCount >= totalVisibleCharacters)
                {
                    yield return new WaitForSeconds(1.0f);
                    // m_textMeshPro.text = label02;
                    // yield return new WaitForSeconds(1.0f);
                    // m_textMeshPro.text = label01;
                    // yield return new WaitForSeconds(1.0f);
                }

                counter += 1;

                yield return new WaitForSeconds(0.05f);
            }
            CR_running = false;
            //Debug.Log("Done revealing the text.");
        }

        
    }

        
        
 }
