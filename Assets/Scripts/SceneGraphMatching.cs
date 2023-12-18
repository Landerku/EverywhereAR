using Microsoft.MixedReality.SceneUnderstanding.Samples.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.SceneUnderstanding;
using System;
using System.Linq;
public class SceneGraphMatching: MonoBehaviour
{
    public List<SceneObject_Unity> GameSceneObjects = new List<SceneObject_Unity>();
    public List<Relation> Relations = new List<Relation>();

    public static bool IsMatchingNode(SceneObject_Unity n, SceneObject_Unity targetNode)
    {
        // Now is strict mode

        if (n.Category == "Wall" && targetNode.Category == "Wall" && targetNode.Size >= n.Size)
            return true;
        if ((n.Category == "Chair" && targetNode.Category == "Background") && (n.Horizontal == targetNode.Horizontal))
            return true;
        if (n.Category == "Table" && targetNode.Category == "Platform")//***For TEST***
            return true;
        if (n.Category == targetNode.Category && Math.Abs(targetNode.Size - n.Size) <= 1)
            return true;
        if (Math.Abs(targetNode.Size - n.Size) <= 0.5 && Math.Abs(targetNode.Height - n.Height) <= 0.2 && n.Horizontal == targetNode.Horizontal && n.Category!="Unknown" && n.Category!="Character")
            return true;

        return false;
    }



    public static bool check_contradtionary(Dictionary<Relation, Relation>  matchRelations, Dictionary<string, RelationType> real_dict, Dictionary<string, RelationType> virtual_dict)
    {
        var virtual_real_matching = new Dictionary<string, string>();
        var real_virtual_matching = new Dictionary<string, string>();

        List<string> virtual_relationship_list = new List<string>();
        foreach (KeyValuePair<Relation, Relation> pair in matchRelations)
        {
            string[] virtual_key_pairs = new string[] { pair.Key.StartNode.Category+pair.Key.StartNode.ID, pair.Key.EndNode.Category + pair.Key.EndNode.ID };
            string[] real_key_pairs = new string[] { pair.Value.StartNode.Category + pair.Value.StartNode.ID, pair.Value.EndNode.Category + pair.Value.EndNode.ID };
            virtual_relationship_list.Add(pair.Key.StartNode.Category + pair.Key.StartNode.ID + "_" + pair.Key.EndNode.Category + pair.Key.EndNode.ID);
            for (int i = 0; i < 2; i++)
            {
                if (virtual_real_matching.ContainsKey(virtual_key_pairs[i]))
                {

                    if (virtual_real_matching[virtual_key_pairs[i]] != real_key_pairs[i])
                    {
                        return false;
                    }

                }
                else 
                {
                    virtual_real_matching[virtual_key_pairs[i]] = real_key_pairs[i];
                }

                if (real_virtual_matching.ContainsKey(real_key_pairs[i]))
                {

                    if (real_virtual_matching[real_key_pairs[i]] != virtual_key_pairs[i])
                    {
                        return false;
                    }

                }
                else
                {
                    real_virtual_matching[real_key_pairs[i]] = virtual_key_pairs[i]; 
                }
            }
        }
        foreach(KeyValuePair<string, RelationType> pair in virtual_dict)
        {
            if (virtual_relationship_list.Contains(pair.Key)) {continue;}
            if ((virtual_real_matching.ContainsKey(pair.Key.Split('_')[0])) & (virtual_real_matching.ContainsKey(pair.Key.Split('_')[1])))  
            {
                if (real_dict.ContainsKey(virtual_real_matching[pair.Key.Split('_')[0]] + "_" + virtual_real_matching[pair.Key.Split('_')[1]]) == false)
                {
                    continue;
                }
                else if (real_dict[virtual_real_matching[pair.Key.Split('_')[0]] + "_" + virtual_real_matching[pair.Key.Split('_')[1]]] != pair.Value)
                {
                    return false;
                }
            }
        }
        return true;
    }


    public static Dictionary<string, RelationType> build_dict(List<Relation> Relations)
    {

        var Node_Node_Relation = new Dictionary<string, RelationType>();
        foreach (Relation relation in Relations) 
        {
            Node_Node_Relation[relation.StartNode.Category + relation.StartNode.ID + "_" + relation.EndNode.Category + relation.EndNode.ID] = relation.Relationship;
        }
        return Node_Node_Relation;
    }

    public static (List<SceneObject_Unity>, List<Relation>) CompareNodes(List<SceneObject_Unity> realSceneNodes, List<SceneObject_Unity> gameSceneNodes, List<Relation> realRelations, List<Relation> gameRelations)
    {
        var resultRelations = new List<Relation>();
        var gameNodeToRealNode = new Dictionary<SceneObject_Unity, SceneObject_Unity>();
        var RealSceneRelations = new List<Relation>(realRelations);
        var matchRelations = new Dictionary<Relation, List<Relation>>();
        var real_dict = build_dict(realRelations);
        var virtual_dict = build_dict(gameRelations);


        foreach (var gameRelation in gameRelations)
        {
            var gameStartNode = gameRelation.StartNode;
            var gameEndNode = gameRelation.EndNode;
            var gameRelationship = gameRelation.Relationship;
            List<RelationType> acceptableRealRelationships = new List<RelationType>();

            switch (gameRelationship)
            {
                case RelationType.RightLeft:
                    acceptableRealRelationships.AddRange(new[] { RelationType.Right, RelationType.Left });
                    break;

                case RelationType.FrontBehind:
                    acceptableRealRelationships.AddRange(new[] { RelationType.InFrontOf, RelationType.Behind });
                    break;

                case RelationType.Around:
                    acceptableRealRelationships.AddRange(new[] { RelationType.Right, RelationType.Left, RelationType.InFrontOf, RelationType.Behind });
                    break;

                default:
                    acceptableRealRelationships.Add(gameRelationship);
                    break;
            }

            var realRelation = realRelations.Find(relation =>
                IsMatchingNode(gameStartNode, relation.StartNode) &&
                IsMatchingNode(gameEndNode, relation.EndNode) &&
                acceptableRealRelationships.Contains(relation.Relationship));

            var testRelation = realRelations.FindAll(relation =>
                IsMatchingNode(gameStartNode, relation.StartNode) &&
                IsMatchingNode(gameEndNode, relation.EndNode) &&
                acceptableRealRelationships.Contains(relation.Relationship));

        
            /*var realRelation = realRelations.Find(relation =>
                IsMatchingNode(gameStartNode, relation.StartNode) &&
                IsMatchingNode(gameEndNode, relation.EndNode) &&
                relation.Relationship == gameRelationship);

            var testRelation = realRelations.FindAll(relation =>
                IsMatchingNode(gameStartNode, relation.StartNode) &&
                IsMatchingNode(gameEndNode, relation.EndNode) &&
                relation.Relationship == gameRelationship);*/

            if (testRelation.Count != 0) //All matched
            {
                matchRelations[gameRelation] = testRelation;
            }

            if (realRelation != null) //All matched

            {
                resultRelations.Add(realRelation);
                RealSceneRelations.Remove(realRelation);
                gameNodeToRealNode[gameStartNode] = realRelation.StartNode;
                gameNodeToRealNode[gameEndNode] = realRelation.EndNode;

            }
            else
            {
                var matchedStartNode = realSceneNodes.Find(n => IsMatchingNode(n, gameStartNode));
                var matchedEndNode = realSceneNodes.Find(n => IsMatchingNode(n, gameEndNode));

                if (matchedStartNode != null)
                {
                    gameNodeToRealNode[gameStartNode] = matchedStartNode;
                }
                if (matchedEndNode != null)
                {
                    gameNodeToRealNode[gameEndNode] = matchedEndNode;
                }
            }
        }


        List<Relation> relationList = new List<Relation>();
        List<int> corresponding_nums = new List<int>();
        List<int> corresponding_total_nums = new List<int>();
        var matchRelations_trail = new Dictionary<Relation, Relation>();
        bool success = false;
        for (int Valid_count = matchRelations.Count; Valid_count > 0; Valid_count--)
        {
            int num = 0;
            int total_match = 1;
            foreach (KeyValuePair<Relation, List<Relation>> pair in matchRelations)
            {
                num++;
                corresponding_nums.Add(pair.Value.Count);
                corresponding_total_nums.Add(total_match);
                total_match *= pair.Value.Count;
                relationList.Add(pair.Key);
                if (num == Valid_count) { break; }
            }

            matchRelations_trail = new Dictionary<Relation, Relation>();
            for (int trial_num = 0; trial_num < total_match; trial_num++)
            {
                matchRelations_trail = new Dictionary<Relation, Relation>();
                for (int i = 0; i < num; i++)
                {
                    matchRelations_trail[relationList[i]] = matchRelations[relationList[i]][(trial_num / corresponding_total_nums[i]) % corresponding_nums[i]];
                }

                success = check_contradtionary(matchRelations_trail, real_dict, virtual_dict);
                if (success) { break;  }
            }
            if (success) { break; }
        }


        Dictionary<string, SceneObject_Unity> virtual_real_matching = new Dictionary<string, SceneObject_Unity>();
        foreach (KeyValuePair<Relation, Relation> pair in matchRelations_trail)
        {
            virtual_real_matching[pair.Key.StartNode.Category + pair.Key.StartNode.ID] = pair.Value.StartNode;
            virtual_real_matching[pair.Key.EndNode.Category + pair.Key.EndNode.ID] = pair.Value.EndNode;
        }

        List<Relation> new_Relation = new List<Relation>();

        foreach (Relation pair in realRelations) { new_Relation.Add(pair);}
        foreach (Relation pair in gameRelations)
        {
            if (matchRelations_trail.ContainsKey(pair)) { continue; }
            if (virtual_real_matching.ContainsKey(pair.StartNode.Category + pair.StartNode.ID)) 
            {
                new_Relation.Add(new Relation(virtual_real_matching[pair.StartNode.Category + pair.StartNode.ID], pair.EndNode, pair.Relationship));
                continue;
            }

            if (virtual_real_matching.ContainsKey(pair.EndNode.Category + pair.EndNode.ID))
            {
                new_Relation.Add(new Relation(pair.StartNode, virtual_real_matching[pair.EndNode.Category + pair.EndNode.ID], pair.Relationship));
                continue;
            }
            new_Relation.Add(pair);
        }


        var allNodes = new_Relation
        .SelectMany(relation => new[] { relation.StartNode, relation.EndNode })
        .Distinct()
        .ToList();

        return (allNodes, new_Relation);
    }

}
