using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


	private Transform player;								// Referencia do componente transform do player (Jack). 

	private Vector2 cameraStartCoord;						// Coordernadas iniciais da camera.

	private float currentTime = 0.0f;						// Tempo total armazenado.
	
	public bool cameraFollowX = true;						// Camera segue no eixo x.
	public float startFollowXAfter = 0.0f;					// Camera segue no eixo x depois de x segundos.
	public float marginX = 0.0f;							// Margem do eixo x.
	public float smoothTimeX = 2.0f;						// Tempo para a camera se deslocar no eixo x.
	public Vector2 xAxisArea;								// Coordenadas adicionais minimas/maximas em que a camera ira seguir no eixo x.
	public Vector2 cameraAreaXAxis;						// Coordenadas minimas/maximas em que a camera ira seguir no eixo x (ja calculada).

	public bool cameraFollowY = true;						// Camera segue no eixo y.
	public float startFollowYAfter = 0.0f;					// Camera segue no eixo y depois de y segundos.
	public float marginY = 0.0f;							// Margem do eixo y.
	public float smoothTimeY = 2.0f;						// Tempo para a camera se deslocar no eixo y.
	public Vector2 yAxisArea;								// Coordenadas adicionais minimas/maximas em que a camera ira seguir no eixo y.
	public Vector2 cameraAreaYAxis;						// Coordenadas minimas/maximas em que a camera ira seguir no eixo y (ja calculada).

	void Awake ()
	{
		// Pegando referencia do jogador.
		//player = GameObject.Find("Nonda").transform;
		player = GameObject.Find("Player").transform;



		cameraStartCoord = new Vector2 (transform.position.x, transform.position.y);
		cameraAreaXAxis = new Vector2 ( (cameraStartCoord.x + xAxisArea.x), (cameraStartCoord.x + xAxisArea.y) );
		cameraAreaYAxis = new Vector2 ( (cameraStartCoord.y + yAxisArea.x), (cameraStartCoord.y + yAxisArea.y) );

		//Verifica se o som deve ser mutado ou nao.
		if (PlayerPrefs.GetInt("Sound") == 1 )
		{
			AudioListener.pause = false;
		}
		else
		{
			AudioListener.pause = true;
		}
	}

	void FixedUpdate () 
	{
		TrackPlayer ();
	}

	void Update ()
	{
		//if(AudioManager.Instance
		//Verifica se o som esta ativado ou nao (evita o bug de minimizar o jogo e voltar o som).
		if (PlayerPrefs.GetInt("Sound") == 0 ) 
		{
			AudioListener.pause = true;
		}
		else if (PlayerPrefs.GetInt("Sound") == 1 ) 
		{
			AudioListener.pause = false;
		}
	}

	bool CheckMarginX()
	{
		// Retorna VERDADEIRO se a distancia entre a camera e o player no eixo X for maior que a margem escolhida.
		return Mathf.Abs(transform.position.x - player.position.x) > marginX;
	}
	
	
	bool CheckMarginY()
	{
		// Retorna VERDADEIRO se a distancia entre a camera e o player no eixo Y for maior que a margem escolhida.
		return Mathf.Abs(transform.position.y - player.position.y) > marginY;
	}

	void TrackPlayer ()
	{
		// A posicao atual das das coordenadas da camera.
		float coordX = transform.position.x;
		float coordY = transform.position.y;
		currentTime += Time.deltaTime;

		if (cameraFollowX && CheckMarginX() && currentTime >= startFollowXAfter)
		{
			coordX = Mathf.Lerp(transform.position.x, player.position.x, smoothTimeX * Time.deltaTime);
		}
		if (cameraFollowY && CheckMarginY() && currentTime >= startFollowYAfter)
		{
			coordY = Mathf.Lerp(transform.position.y, player.position.y, smoothTimeY * Time.deltaTime);
		}

		// As coordernadas do eixo X e Y nao devem ser maior do que as coordenadas da area de alcance da camera (xAxisArea e yAxisArea).
		coordX = Mathf.Clamp( coordX, cameraAreaXAxis.x, cameraAreaXAxis.y );
		coordY = Mathf.Clamp( coordY, cameraAreaYAxis.x, cameraAreaYAxis.y );

		// Seta a nova posicao da camera seguindo as preferencias escolhidas.
		transform.position = new Vector2(coordX, coordY);
	}
}
