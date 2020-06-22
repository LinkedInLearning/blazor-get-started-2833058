window.animatedBeam = {
    loadAnimation: function (elementId, width, height) {
        var renderer, mesh, position, scene, camera;

        position = -1.5;
        var element = document.getElementById(elementId);
        camera = new THREE.PerspectiveCamera(20, width / height, 0.05, 10);
        camera.position.z = 1;

        scene = new THREE.Scene();
        scene.background = new THREE.Color(0xffffff);

        var pointLight = new THREE.PointLight(0xffFFFF, .5);
        pointLight.position.set(1, 1, 1);
        scene.add(pointLight);
        var spotLight = new THREE.SpotLight(0xff0000, 1);
        spotLight.position.set(0, 0, 10);
        var spotLight2 = new THREE.SpotLight(0xff0000, 0.5);
        spotLight2.position.set(0, -10, -10);
        scene.add(spotLight);
        scene.add(spotLight2);

        var geometry = new THREE.BoxGeometry(0.4, 0.03, 0.03);
        var material = new THREE.MeshPhongMaterial({
            color: 0xF3FFE2,
            specular: 0xffffff,
            shininess: 100
        });

        mesh = new THREE.Mesh(geometry, material);
        scene.add(mesh);

        renderer = new THREE.WebGLRenderer({ antialias: true });
        renderer.setSize(width, height);
        element.appendChild(renderer.domElement);

        animatedBeam.animate(renderer, mesh, position, scene, camera);
    },

    animate: function (renderer, mesh, position, scene, camera) {

        requestAnimationFrame(function () { animatedBeam.animate(renderer, mesh, position, scene, camera); });

        mesh.rotation.x += 0.01;
        position = position + .01;
        if (position >= 1.2) {
            position = -1.5;
            DotNet.invokeMethodAsync("Beam.Client", "BeamPassedBy");
        }
        mesh.position.set(position, 0, 0);
        renderer.render(scene, camera);
    }
}
