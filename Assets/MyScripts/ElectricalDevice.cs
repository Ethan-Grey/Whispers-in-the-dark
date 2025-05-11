using UnityEngine;
using UnityEngine.Events;

namespace UHFPS.Runtime
{
    public class ElectricalDevice : MonoBehaviour
    {
        public Light[] lights;
        public GameObject[] activateObjects;
        public ParticleSystem[] particleSystems;
        public Animator[] animators;
        public Renderer[] emissiveMeshes;
        
        public string animationTriggerOn = "PowerOn";
        public string animationTriggerOff = "PowerOff";
        
        public UnityEvent onPowerOn;
        public UnityEvent onPowerOff;
        
        // Add this flag to track power state
        public bool isPoweredOn { get; private set; } = false;
        
        // Start in powered off state
        private void Start()
        {
            // Start with everything powered off, but keep objects visible
            InitializePoweredOffState();
        }
        
        private void InitializePoweredOffState()
        {
            // Disable lights but keep them visible
            foreach (var light in lights)
            {
                if (light != null)
                {
                    light.enabled = false;
                }
            }
            
            // Keep objects visible but disable specific functionality
            foreach (var obj in activateObjects)
            {
                if (obj != null)
                {
                    // Disable scripts but keep object visible
                    var components = obj.GetComponents<MonoBehaviour>();
                    foreach (var component in components)
                    {
                        if (component != this) // Don't disable self
                            component.enabled = false;
                    }
                }
            }
            
            // Make sure particle systems are stopped
            foreach (var ps in particleSystems)
            {
                if (ps != null)
                {
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    ps.Clear();
                }
            }
            
            // Set animators to powered-off state
            foreach (var anim in animators)
            {
                if (anim != null)
                {
                    anim.SetTrigger(animationTriggerOff);
                }
            }
            
            // Turn off emission on materials
            foreach (var mesh in emissiveMeshes)
            {
                if (mesh != null && mesh.material.HasProperty("_EmissionColor"))
                {
                    mesh.material.SetColor("_EmissionColor", Color.black);
                    mesh.material.DisableKeyword("_EMISSION");
                }
            }
            
            // Invoke additional off behavior
            onPowerOff?.Invoke();
            
            isPoweredOn = false;
        }
        
        public void PowerOn()
        {
            if (isPoweredOn) return;
            
            // Enable lights
            foreach (var light in lights)
            {
                if (light != null)
                    light.enabled = true;
            }
            
            // Enable objects
            foreach (var obj in activateObjects)
            {
                if (obj != null)
                {
                    // Re-enable scripts
                    var components = obj.GetComponents<MonoBehaviour>();
                    foreach (var component in components)
                    {
                        if (component != this) // Don't enable self twice
                            component.enabled = true;
                    }
                }
            }
            
            // Play particle systems
            foreach (var ps in particleSystems)
            {
                if (ps != null)
                    ps.Play();
            }
            
            // Trigger animations
            foreach (var anim in animators)
            {
                if (anim != null)
                    anim.SetTrigger(animationTriggerOn);
            }
            
            // Enable emission on materials
            foreach (var mesh in emissiveMeshes)
            {
                if (mesh != null && mesh.material.HasProperty("_EmissionColor"))
                {
                    mesh.material.SetColor("_EmissionColor", mesh.material.GetColor("_EmissionColor") * 2f);
                    mesh.material.EnableKeyword("_EMISSION");
                }
            }
            
            // Invoke custom events
            onPowerOn?.Invoke();
            
            isPoweredOn = true;
        }
        
        public void PowerOff()
        {
            if (!isPoweredOn) return;
            
            // Call the same initialization method
            InitializePoweredOffState();
        }
    }
}