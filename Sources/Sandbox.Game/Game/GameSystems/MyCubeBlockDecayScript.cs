﻿using Sandbox.Definitions;
using Sandbox.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRage.Collections;
using VRage.Game.Systems;
using VRage.Utils;

namespace Sandbox.Game.GameSystems
{
    [MyScriptedSystem("DecayBlocks")]
    public class MyCubeBlockDecayScript : MyGroupScriptBase
    {
        private HashSet<MyStringHash> m_tmpSubtypes;

        public MyCubeBlockDecayScript()
            :
            base()
        {
            m_tmpSubtypes = new HashSet<MyStringHash>(MyStringHash.Comparer);
        }

        public override void ProcessObjects(ListReader<MyDefinitionId> objects)
        {
            var allEntities = MyEntities.GetEntities();

            m_tmpSubtypes.Clear();
            foreach (var obj in objects)
                m_tmpSubtypes.Add(obj.SubtypeId);

            foreach (var entity in allEntities)
            {
                if (!(entity is MyCubeGrid))
                    continue;

                var cubeGrid = entity as MyCubeGrid;
                if (cubeGrid.BlocksCount != 1)
                    continue;

                if (cubeGrid.IsStatic)
                    continue;

                if (MyManipulationTool.IsEntityManipulated(entity))
                    continue;

                var blocks = cubeGrid.CubeBlocks;
                var block = blocks.First();

                if (m_tmpSubtypes.Contains(block.BlockDefinition.Id.SubtypeId))
                {
                    entity.SyncObject.SendCloseRequest();
                }
            }
        }
    }
}
