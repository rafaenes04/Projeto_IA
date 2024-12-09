using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.AI;
using System;

public class MazeAgent : Agent
{
    [SerializeField] private Transform target; // Destino final do labirinto.

    private Vector3 initialPosition = new Vector3(4.4000001f,0.400000006f,-2.5999999f);

    public override void CollectObservations(VectorSensor sensor)
    {
        // Adiciona observações para o agente:
        // - Posição do agente
        // - Posição do destino
        // - Velocidade do agente
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
    }

    public override void OnEpisodeBegin()
    {
        // Reseta o agente no início de cada episódio.
        transform.localPosition = initialPosition;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Converte ações para movimento no labirinto:
        float moveX = actions.ContinuousActions[0]; // Movimento no eixo X.
        float moveZ = actions.ContinuousActions[1]; // Movimento no eixo Z.

        float moveSpeed = 1f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter(Collider other){
        if(other.TryGetComponent<Goal>(out Goal goal)){
            SetReward(+1f);
            EndEpisode();
        }
        if(other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-1f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Controle manual para testar:
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
    }

/*    private Vector3 GetRandomPositionInMaze()
    {
        // Gere uma posição aleatória no labirinto (dentro dos limites).
        float x = Random.Range(-10f, 10f); // Ajuste os limites conforme o tamanho do labirinto.
        float z = Random.Range(-10f, 10f);
        return new Vector3(x, 0.5f, z);
    }*/
}
