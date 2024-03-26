#pragma once
#include <SFML/Graphics.hpp>
#include "Constants.h"
#include "Math.h"

namespace Contra
{
	enum class PlayerDirection
	{
		Right = 0,
		Up,
		Left
	};

	struct Player
	{
		Position2D playerPosition;
		float playerSpeed = INITIAL_SPEED;
		PlayerDirection playerDirection = PlayerDirection::Right;
		sf::Sprite playerSprite;
	};

	//void InitPlayer(Player& player, const Game& game);
}