Prism.EventAggregator
=====================

A simple event aggregator, i.e. a publisher/subscriber mechanism
without strong coupling.

![EventAggregator Image](https://github.com/tngraf/Prism.EventAggregator/blob/master/doc/EventAggregator.png)

Once I required some kind of event aggregator. I browsed the web for ideas
for the best approach. But there were far too many solution only for specific
aspects or wit some severe drawback (for example like not using weak events).
At the end I thought, why should I write my own code when a widely used
solution is already available as open source? 

So I decided to extract the event aggregator part of **Microsoft Prism** and place it in its own small library.


## Project Build Status ##
[![Build status](https://ci.appveyor.com/api/projects/status/d3l2lru8k52j4hd7?svg=true)](https://ci.appveyor.com/project/tngraf/prism-eventaggregator/branch/master) 
[![License](https://img.shields.io/badge/license-Apache--2.0-blue.svg)](http://www.apache.org/licenses/LICENSE-2.0)

## Build ##

### Requisites ###

* Visual Studio 2013
* Microsoft .Net Framework 4.5.1

## Thanks ##

Well, the idea and most of the code is from Microsofts patterns&practices group. This very code has been taken from [https://compositewpf.codeplex.com/](https://compositewpf.codeplex.com/),
Prism V5, commit ff6316df3dad of Aug 22, 2014.

## Copyright & License ##

Copyright (c) Microsoft Corporation. All rights reserved.

Copyright for modification and demo T. Graf.

Licensed under the **Apache License, Version 2.0** (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and limitations under the License.