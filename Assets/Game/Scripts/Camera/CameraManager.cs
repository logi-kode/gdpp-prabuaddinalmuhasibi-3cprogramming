using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] public CameraState CameraState;
    [SerializeField] private CinemachineCamera _fpsCamera;
    [SerializeField] private CinemachineCamera _tpsCamera;
    [SerializeField] private InputManager _inputManager;

    private void Start()
    {
        _inputManager.OnChangePOVInput += SwitchCamera;
    }

    private void OnDestroy()
    {
        _inputManager.OnChangePOVInput -= SwitchCamera;
    }

    public void SetFPSClampedCamera(bool isClamped, Vector3 playerRotation)
    {
        CinemachinePanTilt pov = _fpsCamera.GetComponent<CinemachinePanTilt>();

        if (isClamped)
        {
            pov.PanAxis.Wrap = false;
            pov.PanAxis.Range = new Vector2(playerRotation.y - 90, playerRotation.y + 90);
        } else
        {
            pov.PanAxis.Range = new Vector2(-180, 180);
            pov.PanAxis.Wrap = true;
        }
    }

    private void SwitchCamera()
    {
        if (CameraState == CameraState.ThirdPerson)
        {
            CameraState = CameraState.FirstPerson;
            _tpsCamera.gameObject.SetActive(false);
            _fpsCamera.gameObject.SetActive(true);
        } else
        {
            CameraState = CameraState.ThirdPerson;
            _tpsCamera.gameObject.SetActive(true);
            _fpsCamera.gameObject.SetActive(false);
        }
    }
}