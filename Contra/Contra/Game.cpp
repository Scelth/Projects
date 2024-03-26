#include "Game.h"

namespace Contra
{
	void InitGame(Game& game, sf::RenderWindow& window)
	{
		InitBackground(game.map);
		InitGround(game.map, window);
	}

	void DrawGame(Game& game, sf::RenderWindow& window)
	{
		window.draw(game.map.backgroundSprite);
		window.draw(game.map.groundSprite);
	}

	void HandleWindowEvents(Game& game, sf::RenderWindow& window)
	{
		sf::Event event;
		while (window.pollEvent(event))
		{
			if (event.type == sf::Event::Closed)
			{
				window.close();
			}

			if ((event.type == sf::Event::KeyPressed) && (event.key.code == sf::Keyboard::Escape))
			{
				window.close();
			}
		}
	}
}