# Taskter-Volatile Decomposition Architecture
<p>
This project it's an attempt to understand Volatile Decomposition Architecture based out of <em><strong>Righting Software</strong></em> by <em><strong>Juval Lowy</strong></em>.<br />
I appreciate the simplicity of keeping change within the respective layers,<br />
being reinforced by contract-based interfaces approach across services that use only what they need.<br />
</p>

<h5>Architecture Decomposition</h5>
<p>
Volotile Decomposition breaks down as follow, in system accessibility in descending order<br/>
Where the top is what Clients must use to encapsulate change and the components that follow can only be accessed by preceding components.
</p>
<ol>
<li>Client => currently a console application, but it could be an API or an actual UI</li>
<li>Manager => The main point of access to the system</li>
<li>ResourceAccessors => Accessed by the manager through its own proxies or contracts</li>
<li>Resources => The actual things the system manages</li>
</ol>

<br />
<h3>Experimentation Applied to project</h3>
<ul> 
<li>Testing suite follows a Builder pattern</li>
<li>Testing suite uses moq library for testing mocks</li>
<li>Testing suite only tests the Manager services since it has proxies to their respective ResourceAccessors</li>
<li>Volotile Decomposition</li>
<li>All third party or domain interfaces are part of the Utilities components and can be used by any component in the architecture</li>
</ul>
