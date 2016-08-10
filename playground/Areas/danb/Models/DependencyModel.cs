using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playground.Areas.Danb.Models
{
    public class DependencyModel
    {
    }
}

/*

    Dependency Analysis for a Package Manager

    Package -> Set of Dependency (unordered)
    MAIN    -> B C D
    B       -> E F
    C       -> B G
    D       -> C F

    Resolves to:  MAIN D C B F G E
    Revere Order For Loading:   E G F B C D MAIN

    */