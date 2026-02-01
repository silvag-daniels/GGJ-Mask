using UnityEngine;

public class TortugaDTO
{
    private float direction;
    private bool arriba;

    public TortugaDTO(float direction, bool arriba) {
        this.direction = direction;
        this.arriba = arriba;
    }

    public float getDirection() {
        return direction;
    }

    public bool getArriba() {
        return arriba;
    }

}