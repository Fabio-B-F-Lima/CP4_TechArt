using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CameraManager : MonoBehaviour
{
    [Header("Fixed Cameras")]
    [SerializeField] private CinemachineCamera camera01;
    [SerializeField] private CinemachineCamera camera02;
    [SerializeField] private CinemachineCamera camera03;
    [SerializeField] private CinemachineCamera camera04;

    [Header("Special Cameras")]
    [SerializeField] private CinemachineCamera freeCamera;
    [SerializeField] private CinemachineCamera dollyCamera;

    private CinemachineCamera[] cameras;
    [SerializeField] private PlayableDirector dollyTimeline;

    private void Start()
    {
        cameras = new CinemachineCamera[]
        {
            camera01,
            camera02,
            camera03,
            camera04,
            freeCamera,
            dollyCamera
        };

        ActivateCamera(freeCamera);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ActivateCamera(camera01);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ActivateCamera(camera02);

        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ActivateCamera(camera03);

        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ActivateCamera(camera04);

        else if (Input.GetKeyDown(KeyCode.P))
        {
            ActivateCamera(dollyCamera);
            PlayDolly();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateCamera(freeCamera);
        }
    }

    private void ActivateCamera(CinemachineCamera activeCamera)
    {
        foreach (CinemachineCamera camera in cameras)
        {
            camera.Priority.Value = 0;
        }

        activeCamera.Priority.Value = 2;
    }
    private void PlayDolly()
    {
        if (dollyTimeline.state == PlayState.Playing)
            return;

        dollyTimeline.time = 0;
        dollyTimeline.Evaluate();
        dollyTimeline.Play();
    }
}
