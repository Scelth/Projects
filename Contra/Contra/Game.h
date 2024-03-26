#pragma once
#include "Constants.h"
#include "Player.h"
#include "Map.h"

namespace Contra
{
	struct Game
	{
		Player player;
		Map map;
	};

	void InitGame(Game& game, sf::RenderWindow& window);
	void DrawGame(Game& game, sf::RenderWindow& window);
	void HandleWindowEvents(Game& game, sf::RenderWindow& window);
}