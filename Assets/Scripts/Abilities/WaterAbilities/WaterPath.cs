using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPath : MonoBehaviour
{
    public GameObject User { set { _user = value; _entityUser = _user.GetComponent<Entity>(); } }
        
    private List<Vector3> _points; // principals points
    private LineRenderer _lineRenderer;
    private bool _enabled;
    private bool _teleporting;
    private GameObject _user;
    private Entity _entityUser;

    void Start()
    {
        _points = new List<Vector3>();
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _enabled = false;
        _teleporting = false;

        AbilityEventManager.waterTeleportStartEvent.AddListener(Teleport);
    }

    private void OnEnable() {
        // when teleporters are destroyed, the path is reseted
        AbilityEventManager.waterPathResetEvent.AddListener(ResetPath);
    }
    private void OnDisable() {
        // removing the callback        
        AbilityEventManager.waterPathResetEvent.RemoveListener(ResetPath);
        AbilityEventManager.waterTeleportStartEvent.RemoveListener(Teleport);
    }

    private IEnumerator AddPoint() {
        while (_enabled) {
            if (_points.Count == 0 || _user.transform.position != _points[_points.Count - 1]) {
                _points.Add(_user.transform.position);
                SetLineRenderer();
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void Teleport(int from) {
        if (!_teleporting) {
            _teleporting = true;
            StartCoroutine(TeleportAnimation(from));
        }
    }

    private IEnumerator TeleportAnimation(int from) {
        _entityUser.UsingMoveAbility = true;
        while (_points.Count != 0) {
            // from last teleporter
            if (from == 1) {
                _user.transform.position = _points[_points.Count - 1];
                _points.RemoveAt(_points.Count - 1);
            } else {
                _user.transform.position = _points[0];
                _points.RemoveAt(0);
            }
            SetLineRenderer();

            yield return new WaitForSeconds(0.01f);
        }
        AbilityEventManager.waterTeleportEndEvent.Invoke();
        _entityUser.UsingMoveAbility = false;
        _teleporting = false;
        ResetPath();
    }

    private void SetLineRenderer() {
        _lineRenderer.positionCount = _points.Count;
        _lineRenderer.SetPositions(_points.ToArray());
    }

    public void StartPath() {
        _enabled = true;
        StartCoroutine(AddPoint());
    }

    public void StopPath() {
        _enabled = false;
        _points.Add(_user.transform.position);
        SetLineRenderer();
    }

    public void ResetPath() {
        if (!_teleporting) {
            _points.Clear();
            SetLineRenderer();
        }
    }
}
