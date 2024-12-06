# DotNet_WebAPI_doc

cmds...

dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-ef

<!-- create app -->
dotnet new webapi -n DotNet9CookieAuthAPI
cd DotNet9CookieAuthAPI


dotnet ef migrations add InitialCreate

<!-- update db -->
dotnet ef database update


<!-- this is how to remove files in git -->
git rm --cached appsettings.json


```svelte
<!-- 1. 🔥 Firebase App -->
<FirebaseApp {auth} {firestore}>

  <!-- 2. 👤 Get the current user -->
  <SignedIn let:user>

    <p>Howdy, {user.uid}</p>

    <!-- 3 (a). 📜 Get a Firestore document owned by a user -->
    <Doc ref={`posts/${user.uid}`} let:data={post} let:ref={postRef}>

      <h2>{post.title}</h2>

      <!-- 4 (a). 💬 Get all the comments in its subcollection -->
      <Collection ref={postRef.path + '/comments'} let:data={comments}>
        {#each comments as comment}

        {/each}
...