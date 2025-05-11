using System.Collections.Generic;
using UnityEngine;

namespace UHFPS.Runtime
{
    public class ElectricalDeviceManager : MonoBehaviour
    {
        public ElectricalCircuitPuzzle circuitPuzzle;
        public List<ElectricalDevice> electricalDevices = new List<ElectricalDevice>();
        
        [Tooltip("Automatically find all electrical devices in the scene")]
        public bool autoFindDevices = true;

        private void Start()
        {
            // If the circuit is not connected on startup, make sure all devices are powered off
            if (circuitPuzzle != null && !circuitPuzzle.isConnected)
            {
                PowerOffAllDevices();
            }
        }
        
        private void Awake()
        {
            // Auto-find the circuit puzzle if not assigned
            if (circuitPuzzle == null)
                circuitPuzzle = FindObjectOfType<ElectricalCircuitPuzzle>();
                
            // Auto-find electrical devices if selected
            if (autoFindDevices)
                electricalDevices.AddRange(FindObjectsOfType<ElectricalDevice>());
        }
        
        private void OnEnable()
        {
            if (circuitPuzzle != null)
            {
                // Subscribe to circuit events
                circuitPuzzle.OnConnected.AddListener(PowerOnAllDevices);
                circuitPuzzle.OnDisconnected.AddListener(PowerOffAllDevices);
                
                // If the circuit is already connected when enabled, power on devices immediately
                if (circuitPuzzle.isConnected)
                    PowerOnAllDevices();
            }
        }
        
        private void OnDisable()
        {
            if (circuitPuzzle != null)
            {
                // Unsubscribe from circuit events
                circuitPuzzle.OnConnected.RemoveListener(PowerOnAllDevices);
                circuitPuzzle.OnDisconnected.RemoveListener(PowerOffAllDevices);
            }
        }
        
        public void PowerOnAllDevices()
        {
            foreach (var device in electricalDevices)
            {
                if (device != null)
                    device.PowerOn();
            }
        }
        
        public void PowerOffAllDevices()
        {
            foreach (var device in electricalDevices)
            {
                if (device != null)
                    device.PowerOff();
            }
        }
        
        // Helper method to add devices at runtime
        public void AddDevice(ElectricalDevice device)
        {
            if (device != null && !electricalDevices.Contains(device))
                electricalDevices.Add(device);
        }
    }
}