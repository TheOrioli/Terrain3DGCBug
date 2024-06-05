extends Node3D

@export var collider_scene: PackedScene
@export var terrain_scene: PackedScene

var _colliders = []
var _terrain: Terrain3D

func _on_spawn_colliders_pressed():
	var cam := get_viewport().get_camera_3d()
	for i in 100:
		var pos = cam.global_position +  -cam.global_basis.z * randf() * 50
		pos.y = _terrain.storage.get_height(pos) + 1
		var col :RigidBody3D = collider_scene.instantiate()
		col.position = pos
		add_child(col)
		_colliders.push_front(col)


func _on_reset_pressed():
	for c in _colliders:
		c.queue_free()
	_colliders.clear()
	_terrain.queue_free()


func _on_button_terrain_pressed():
	_terrain = terrain_scene.instantiate()
	add_child(_terrain)
