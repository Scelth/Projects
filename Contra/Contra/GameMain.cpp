#include <SFML/Graphics.hpp>
#include <SFML/Audio.hpp>
#include "Constants.h"
#include "Game.h"

int main()
{
	using namespace Contra;

	sf::RenderWindow window(sf::VideoMode(SCREEN_WIDTH, SCREEN_HEIGTH), "Contra");

	Game game;

	InitGame(game, window);

	while (window.isOpen())
	{
		HandleWindowEvents(game, window);

		window.clear();
		DrawGame(game, window);
		window.display();
	}

	return 0;
}
