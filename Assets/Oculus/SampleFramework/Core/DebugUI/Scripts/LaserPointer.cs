/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using Photon.Pun;
using Photon.Realtime;
using Unity.Mathematics;

public class LaserPointer : OVRCursor
{
    public enum LaserBeamBehavior
    {
        On, // laser beam always on
        Off, // laser beam always off
        OnWhenHitTarget, // laser beam only activates when hit valid target
    }
    public GameObject ping;
    public GameObject cursorVisual;
    public GameObject[] pings;
    public int i = 0;
    public float maxLength = 10.0f;
    public float emojidelay = 1f;
    public float emojicool = 3f;
    public bool emojiready = true;
    public GameObject PV;
    private LaserBeamBehavior _laserBeamBehavior;
    bool m_restoreOnInputAcquired = false;

    public LaserBeamBehavior laserBeamBehavior
    {
        set
        {
            _laserBeamBehavior = value;
            if (laserBeamBehavior == LaserBeamBehavior.Off || laserBeamBehavior == LaserBeamBehavior.OnWhenHitTarget)
            {
                lineRenderer.enabled = false;
            }
            else
            {
                lineRenderer.enabled = true;
            }
        }
        get { return _laserBeamBehavior; }
    }

    private Vector3 _startPoint;
    private Vector3 _forward;
    private Vector3 _endPoint;
    private bool _hitTarget;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void Start()
    {
        if (cursorVisual) cursorVisual.SetActive(false);
        OVRManager.InputFocusAcquired += OnInputFocusAcquired;
        OVRManager.InputFocusLost += OnInputFocusLost;
    }

    public override void SetCursorStartDest(Vector3 start, Vector3 dest, Vector3 normal)
    {
        _startPoint = start;
        _endPoint = dest;
        _hitTarget = true;
    }

    public override void SetCursorRay(Transform t)
    {
        _startPoint = t.position;
        _forward = t.forward;
        _hitTarget = false;
    }

    private void LateUpdate()
    {

            if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch))
            {
                i += 1;
                if (i >= 3)
                {
                    i = 0;
                }
            }
            lineRenderer.SetPosition(0, _startPoint);
            if (_hitTarget)
            {
                lineRenderer.SetPosition(1, _endPoint);
                UpdateLaserBeam(_startPoint, _endPoint);
                if (cursorVisual)
                {
                    cursorVisual.transform.position = _endPoint;
                    cursorVisual.SetActive(true);
                    lineRenderer.enabled = true;
                }

            }

            else
            {
                // If the cursor doesn't hit a target, dynamically calculate the laser beam length
                RaycastHit hit;
                if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
                {

                    if (Physics.Raycast(_startPoint, _forward, out hit, maxLength))
                    {
                        // If the ray hits something within maxLength, update the laser beam to reach the hit point
                        lineRenderer.SetPosition(1, hit.point);
                        UpdateLaserBeam(_startPoint, hit.point);
                        lineRenderer.enabled = true;
                        cursorVisual.transform.position = hit.point;
                        cursorVisual.SetActive(true);
                        emojicool += Time.deltaTime;
                        emojiready = emojidelay < emojicool;//딜레이 수정 함수로
                                                            // PV.RPC("Ping", RpcTarget.All,null);
                        Ping();
                        ping.transform.position = hit.point;
                    }

                    /*  if (cursorVisual)
                      {
                          cursorVisual.transform.position = hit.point;
                          cursorVisual.SetActive(true);
                          lineRenderer.enabled = true;
                      }*/

                }
                else
                {
                    Vector3 endPoint = _startPoint + maxLength * _forward;
                    lineRenderer.SetPosition(1, endPoint);
                    UpdateLaserBeam(_startPoint, endPoint);

                    if (cursorVisual)
                    {
                        cursorVisual.SetActive(false);
                        lineRenderer.enabled = false;
                        // ping.SetActive(false);
                    }
                }
            }
        }

    // make laser beam a behavior with a prop that enables or disables
    private void UpdateLaserBeam(Vector3 start, Vector3 end)
    {
        if (laserBeamBehavior == LaserBeamBehavior.Off)
        {
            return;
        }
        else if (laserBeamBehavior == LaserBeamBehavior.On)
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }
        else if (laserBeamBehavior == LaserBeamBehavior.OnWhenHitTarget)
        {
            if (_hitTarget)
            {
                if (!lineRenderer.enabled)
                {
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, start);
                    lineRenderer.SetPosition(1, end);
                }
            }
            else
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
            }
        }
    }

    void OnDisable()
    {
        if (cursorVisual) cursorVisual.SetActive(false);
    }

    public void OnInputFocusLost()
    {
        if (gameObject && gameObject.activeInHierarchy)
        {
            m_restoreOnInputAcquired = true;
            gameObject.SetActive(false);
        }
    }

    public void OnInputFocusAcquired()
    {
        if (m_restoreOnInputAcquired && gameObject)
        {
            m_restoreOnInputAcquired = false;
            gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        OVRManager.InputFocusAcquired -= OnInputFocusAcquired;
        OVRManager.InputFocusLost -= OnInputFocusLost;
    }

    public void Ping()
    {
        Transform Tr = Camera.main.transform;
        Quaternion rotation = Quaternion.LookRotation(Tr.forward);
        switch (i)
        {
            case 0:
                PhotonNetwork.Instantiate(pings[0].name, ping.transform.position, rotation);
                break;
            case 1:
                PhotonNetwork.Instantiate(pings[3].name, ping.transform.position, rotation);
                break;
            case 2:
                PhotonNetwork.Instantiate(pings[4].name, ping.transform.position, rotation);
                break;
        }
    }
}
