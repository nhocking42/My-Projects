using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip attack_hand, attack_weapon, bad_flower, big_salamander, break_rock, break_tree, cruel_insect,
        eat, health_potion, hit_rock, hit_tree, kill_mob, old_golem, open_features, pickup_item, rain, red_bird,
        cow, chicken, pig, sheep, player_take_damage, player_die;
    static AudioSource audioSrc;
    void Start()
    {
        attack_hand = Resources.Load<AudioClip>("attack_hand");
        attack_weapon = Resources.Load<AudioClip>("attack_weapon");
        bad_flower = Resources.Load<AudioClip>("bad_flower");
        big_salamander = Resources.Load<AudioClip>("big_salamander");
        break_rock = Resources.Load<AudioClip>("break_rock");
        break_tree = Resources.Load<AudioClip>("break_tree");
        cruel_insect = Resources.Load<AudioClip>("cruel_insect");
        eat = Resources.Load<AudioClip>("eat");
        health_potion = Resources.Load<AudioClip>("health_potion");
        hit_rock = Resources.Load<AudioClip>("hit_rock");
        hit_tree = Resources.Load<AudioClip>("hit_tree");
        kill_mob = Resources.Load<AudioClip>("kill_mob");
        old_golem = Resources.Load<AudioClip>("old_golem");
        open_features = Resources.Load<AudioClip>("open_features");
        pickup_item = Resources.Load<AudioClip>("pickup_item");
        rain = Resources.Load<AudioClip>("rain");
        red_bird = Resources.Load<AudioClip>("red_bird");

        cow = Resources.Load<AudioClip>("cow");
        chicken = Resources.Load<AudioClip>("chicken");
        sheep = Resources.Load<AudioClip>("sheep");
        pig = Resources.Load<AudioClip>("pig");
        player_take_damage = Resources.Load<AudioClip>("player_take_damage");
        player_die = Resources.Load<AudioClip>("player_die");

        audioSrc = GetComponent<AudioSource>();

    }

    void Update()
    {
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "attack_hand":     
                audioSrc.PlayOneShot(attack_hand);
                break;
            case "attack_weapon":
                audioSrc.PlayOneShot(attack_weapon);
                break;
            case "bad_flower":
                audioSrc.PlayOneShot(bad_flower);
                break;
            case "big_salamander":
                audioSrc.PlayOneShot(big_salamander);
                break;
            case "break_rock":
                audioSrc.PlayOneShot(break_rock);
                break;
            case "break_tree":
                audioSrc.PlayOneShot(break_tree);
                break;
            case "cruel_insect":
                audioSrc.PlayOneShot(cruel_insect);
                break;
            case "eat":
                audioSrc.PlayOneShot(eat);
                break;
            case "health_potion":
                audioSrc.PlayOneShot(health_potion);
                break;
            case "hit_rock":
                audioSrc.PlayOneShot(hit_rock);
                break;
            case "hit_tree":
                audioSrc.PlayOneShot(hit_tree);
                break;
            case "kill_mob":
                audioSrc.PlayOneShot(kill_mob);
                break;
            case "old_golem":
                audioSrc.PlayOneShot(old_golem);
                break;
            case "open_features":
                audioSrc.PlayOneShot(open_features);
                break;
            case "pickup_item":
                audioSrc.PlayOneShot(pickup_item);
                break;
            case "rain":
                audioSrc.PlayOneShot(rain);
                break;
            case "red_bird":
                audioSrc.PlayOneShot(red_bird);
                break;
            case "cow":
                audioSrc.PlayOneShot(cow);
                break;
            case "chicken":
                audioSrc.PlayOneShot(chicken);
                break;
            case "sheep":
                audioSrc.PlayOneShot(sheep);
                break;
            case "pig":
                audioSrc.PlayOneShot(pig);
                break;
            case "player_take_damage":
                audioSrc.PlayOneShot(player_take_damage);
                break;
            case "player_die":
                audioSrc.PlayOneShot(player_die);
                break;



        }
    }
}
