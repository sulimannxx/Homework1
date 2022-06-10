using TMPro;
using UnityEngine;

public class MaxCapacityText : MonoBehaviour
{
   [SerializeField] private Warehouse _warehouse;
   [SerializeField] private TMP_Text _text;

   private void Start()
   {
       _text.text = _warehouse.MaxCapacity.ToString();
       _warehouse.CapacityChanged += OnCapacityChanged;
   }

   private void OnCapacityChanged()
   {
       _text.text = _warehouse.MaxCapacity.ToString();
   }

   private void OnDestroy()
   {
       _warehouse.CapacityChanged -= OnCapacityChanged;
   }
}
