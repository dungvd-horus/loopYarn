using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Task.Run( DoWorkParallel );
    }

    private void OnDestroy()
    { 
        
    }

    async Task DoWorkParallel()
        {
            Debug.Log( "parralel calls" );
            TickParallel();
            TockParallel();
        }
    //Task method will work but not if you are not about to wait for it keep it void
    async Task TickParallel()
        {
            Debug.Log( "Parallel Tick Waiting 0.5f second..." );
            await Task.Delay( TimeSpan.FromSeconds( 0.5f ) );
            Debug.Log( "Parallel Tick Done!" );
        }
    //void as return value
    async void TockParallel()
        {
            Debug.Log( "Parallel Tock Waiting 0.5f second..." );
            await Task.Delay( TimeSpan.FromSeconds( 0.5f ) );
            Debug.Log( "Parallel Tock Done!" );
        }
}
